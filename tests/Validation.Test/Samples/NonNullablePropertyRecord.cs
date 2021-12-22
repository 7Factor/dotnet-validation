// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace _7Factor.Validation.Samples;

public record NonNullablePropertyRecord
{
    public string Property { get; init; } = null!;
}
