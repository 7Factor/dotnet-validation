// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace _7Factor.Validation.Samples;

public record NonNullablePropertyClass
{
    public string Property { get; init; } = null!;
}
