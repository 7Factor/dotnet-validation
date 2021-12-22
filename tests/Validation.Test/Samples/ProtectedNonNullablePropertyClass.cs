namespace _7Factor.Validation.Samples;

public class ProtectedNonNullablePropertyClass
{
    public ProtectedNonNullablePropertyClass(string? protectedProperty = null)
    {
        if (protectedProperty is not null)
        {
            Property = protectedProperty;
        }
    }

    protected string Property { get; }
}
