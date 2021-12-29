// ReSharper disable UnusedAutoPropertyAccessor.Global
using System.Diagnostics.CodeAnalysis;

namespace _7Factor.Validation.Samples;

public class NotNullNullablePropertyClass
{
    [NotNull] public string? Property { get; init; }
}
