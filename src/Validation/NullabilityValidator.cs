using System.Collections;
using System.Reflection;

namespace _7Factor.Validation;

/// <summary>
/// Tools for validating objects based on the nullability (or lack thereof) of their properties. This is sometimes
/// required due to objects being created by reflection such as via Serialization/Deserialization. In such cases, it is
/// possible to end up with an instance of a class that contains a non-nullable property but the value of the property
/// is null.
/// </summary>
public class NullabilityValidator
{
    private static readonly NullabilityInfoContext NullabilityInfoContext = new();

    /// <summary>
    /// Validates whether or not all of the properties with public getters of the given object return values that
    /// adhere to the nullability of their types. For example, in a <c>#nullable enable</c> context, if a
    /// property's type is a non-nullable reference type, and the value of the property is null, then validation fails.
    /// This validation is recursive, such that types that have their own properties will be checked as well. This
    /// includes elements in types that implement IEnumerable. However, behavior on an IDictionary type is unspecified.
    /// Value type properties are not validated.
    /// </summary>
    /// <param name="o">The object to validate.</param>
    /// <exception cref="NonNullableReferenceIsNullException">When validation fails.</exception>
    public static void ValidatePropertyReferences(object o)
    {
        ValidatePropertyReferences(o, null);
    }

    private static void ValidatePropertyReferences(object o, string? parentPath)
    {
        new NullabilityValidator(o, parentPath).Validate();
    }

    // The object being validated.
    private readonly object _object;

    // The type of the object being validated.
    private readonly Type _objectType;

    // A string holding the property path to this object or null if it's the top level being validated.
    private readonly string? _parentPath;

    private NullabilityValidator(object o, string? parentPath)
    {
        _object = o;
        _objectType = o.GetType();
        _parentPath = parentPath;
    }

    private void Validate()
    {
        foreach (var prop in _objectType.GetProperties())
        {
            var nullabilityInfo = NullabilityInfoContext.Create(prop);
            if (nullabilityInfo.Type.IsValueType) continue;

            ValidatePropertyReference(prop, nullabilityInfo, prop.GetValue(_object));
        }
    }

    private void ValidatePropertyReference(PropertyInfo prop, NullabilityInfo nullabilityInfo, object? propValue)
    {
        var propTypeIsNonNullable = nullabilityInfo.WriteState == NullabilityState.NotNull;

        if (propTypeIsNonNullable && propValue is null)
        {
            throw new NonNullableReferenceIsNullException(CreateExceptionPropName(prop));
        }

        switch (propValue)
        {
            case null:
                return;
            case IEnumerable elems:
            {
                ValidateEnumerableProperty(prop, nullabilityInfo, elems);

                break;
            }
            default:
            {
                if (prop.PropertyType.Assembly == _objectType.Assembly)
                {
                    // prop is object potentially with its own properties.
                    ValidatePropertyReferences(propValue, $"{prop.Name}.");
                }

                break;
            }
        }
    }

    private void ValidateEnumerableProperty(MemberInfo prop, NullabilityInfo nullabilityInfo, IEnumerable elems)
    {
        var enumerableGenericTypeIsNonNullable = IsGenericTypeNonNullable(nullabilityInfo);

        var i = 0;
        var isList = elems is IList;

        foreach (var elem in elems)
        {
            var e = new EnumerablePropertyElement(elem, enumerableGenericTypeIsNonNullable, isList ? i : null);
            ValidateEnumerableElement(prop, e);

            i++;
        }
    }

    private string CreateExceptionPropName(MemberInfo prop)
    {
        return _parentPath is not null ? $"{_parentPath}{prop.Name}" : prop.Name;
    }

    private void ValidateEnumerableElement(MemberInfo enumerableProp, EnumerablePropertyElement elem)
    {
        if (elem.TypeIsNonNullable && elem.Value is null)
        {
            throw new NonNullableReferenceIsNullException($"{CreateExceptionPropName(enumerableProp)}[{elem.Index}]");
        }

        if (elem.Value is not null && elem.TypeAssembly == _objectType.Assembly)
        {
            ValidatePropertyReferences(elem.Value, $"{enumerableProp.Name}[{elem.Index}].");
        }
    }

    private record EnumerablePropertyElement(object? Value, bool TypeIsNonNullable, string Index)
    {
        internal EnumerablePropertyElement(object? value, bool typeIsNonNullable, int? index) : this(value,
            typeIsNonNullable, index.HasValue ? index.Value.ToString() : "")
        {
        }

        public Assembly? TypeAssembly => Value?.GetType().Assembly;
    }

    private static bool IsGenericTypeNonNullable(NullabilityInfo nullabilityInfo)
    {
        var typeArgsNullabilityInfo = nullabilityInfo.GenericTypeArguments;
        return typeArgsNullabilityInfo.Length > 0 && typeArgsNullabilityInfo[0].WriteState == NullabilityState.NotNull;
    }
}
