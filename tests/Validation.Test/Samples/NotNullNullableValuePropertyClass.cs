// ReSharper disable UnusedAutoPropertyAccessor.Global
using System.Diagnostics.CodeAnalysis;

namespace _7Factor.Validation.Samples;

public class NotNullNullableValuePropertyClass
{
    [NotNull] public int? Property { get; init; }
}
