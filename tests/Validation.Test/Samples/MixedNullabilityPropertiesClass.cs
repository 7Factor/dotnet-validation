// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace _7Factor.Validation.Samples;

public class MixedNullabilityPropertiesClass
{
    public string NonNullableProp { get; init; } = null!;

    public string? NullableProp { get; init; }
}
