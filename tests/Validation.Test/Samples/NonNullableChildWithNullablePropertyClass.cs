// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace _7Factor.Validation.Samples;

public class NonNullableChildWithNullablePropertyClass
{
    public NullablePropertyClass Child { get; init; } = null!;
}
