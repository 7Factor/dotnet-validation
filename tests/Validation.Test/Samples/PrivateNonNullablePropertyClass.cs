namespace _7Factor.Validation.Samples;

public class PrivateNonNullablePropertyClass
{
    public PrivateNonNullablePropertyClass(string? protectedProperty = null)
    {
        if (protectedProperty is not null)
        {
            Property = protectedProperty;
        }
    }

    private string Property { get; }
}
