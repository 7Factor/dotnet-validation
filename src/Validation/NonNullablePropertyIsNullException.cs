using System.Runtime.Serialization;

namespace _7Factor.Validation;

/// <summary>
/// Represents an error in the validation of non-nullable properties. Non-nullable properties are properties that are
/// either non-nullable reference types or nullable value types marked with the [NotNull] attribute.
/// </summary>
[Serializable]
public class NonNullablePropertyIsNullException : Exception
{
    public static readonly string MessageFormat = "The non-nullable property '{0}' is null.";

    public NonNullablePropertyIsNullException() : base("A non-nullable property is null.")
    {
    }

    public NonNullablePropertyIsNullException(string propName) : base(
        string.Format(MessageFormat, propName))
    {
    }

    public NonNullablePropertyIsNullException(string propName, Exception inner) : base(
        string.Format(MessageFormat, propName), inner)
    {
    }

    protected NonNullablePropertyIsNullException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }
}
