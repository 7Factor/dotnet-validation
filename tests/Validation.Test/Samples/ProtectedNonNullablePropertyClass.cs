// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace _7Factor.Validation.Samples;

public class ProtectedNonNullablePropertyClass
{
    public ProtectedNonNullablePropertyClass(string? protectedProperty = null)
    {
        Property = protectedProperty!;
    }

    protected string Property { get; }
}
