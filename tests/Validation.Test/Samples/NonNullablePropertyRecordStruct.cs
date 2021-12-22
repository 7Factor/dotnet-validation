// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace _7Factor.Validation.Samples;

public record struct NonNullablePropertyRecordStruct
{
    public string Property { get; init; } = null!;
}
