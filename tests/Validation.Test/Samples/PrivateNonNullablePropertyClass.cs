// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace _7Factor.Validation.Samples;

public class PrivateNonNullablePropertyClass
{
    public PrivateNonNullablePropertyClass(string? protectedProperty = null)
    {
        Property = protectedProperty!;
    }

    private string Property { get; }
}
