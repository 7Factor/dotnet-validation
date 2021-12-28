using System.Runtime.Serialization;

namespace _7Factor.Validation;

/// <summary>
/// Represents an error in the validation of Non-Nullable Reference Types.
/// </summary>
[Serializable]
public class NonNullableReferenceIsNullException : Exception
{
    public static readonly string MessageFormat = "The non-nullable reference property '{0}' is null.";

    public NonNullableReferenceIsNullException() : base("A non-nullable reference is null.")
    {
    }

    public NonNullableReferenceIsNullException(string propName) : base(
        string.Format(MessageFormat, propName))
    {
    }

    public NonNullableReferenceIsNullException(string propName, Exception inner) : base(
        string.Format(MessageFormat, propName), inner)
    {
    }

    protected NonNullableReferenceIsNullException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }
}
