// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace _7Factor.Validation.Samples;

public class EnumerableNullableElementsClass
{
    public IEnumerable<NonNullablePropertyClass?> Elements { get; init; } = null!;
}
