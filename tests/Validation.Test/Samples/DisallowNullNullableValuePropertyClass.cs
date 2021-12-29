// ReSharper disable UnusedAutoPropertyAccessor.Global
using System.Diagnostics.CodeAnalysis;

namespace _7Factor.Validation.Samples;

public class DisallowNullNullableValuePropertyClass
{
    [DisallowNull] public int? Property { get; init; }
}
