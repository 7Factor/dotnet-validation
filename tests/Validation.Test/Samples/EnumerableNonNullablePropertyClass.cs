// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace _7Factor.Validation.Samples;

public class EnumerableNonNullablePropertyClass
{
    public IEnumerable<NonNullablePropertyClass> Elements { get; init; } = null!;
}
