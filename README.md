# DotnetValidation

A simple library for validating objects.

Currently this library supports validating that the public properties of an object are
not null if that property's type is a non-nullable reference type (in a `#nullable enable`
context.) This is sometimes needed to validate objects after they have been created via
reflection (such as from JSON deserialization.)


## Usage

First, you will need to add the NuGet package `7Factor.Validation` to your project. Then,
using the `_7Factor.Validation` namespace, simply pass the object you wish to validate to
`NullabilityValidator.ValidateProperties()`. If the properties of the object violate their
type's nullability then a `NonNullablePropertyIsNullException` will be thrown with a
message indicating the property that caused the validation to fail.

## Validation Examples

The following examples will likely produce compiler warnings but that is not why this tool
exists. Keep in mind that most JSON deserializers use reflection to set properties and
that is not something the compiler can warn you about.

```c#
#nullable enable

public class Foo {
    public string NonNullable { get; set; }
}

public class Bar {
    public string? Nullable { get; set; }
}

public class FooBar {
    public Foo? Baz { get; set; }
}

// Valid - no exception thrown
NullabilityValidator.ValidateProperties(new Bar());
NullabilityValidator.ValidateProperties(new Foo { NonNullable = "quux" });
NullabilityValidator.ValidateProperties(new FooBar());

// Invalid - throws NonNullablePropertyIsNullException
NullabilityValidator.ValidateProperties(new Foo()); // exception message points to "NonNullable" property
NullabilityValidator.ValidateProperties(new FooBar { Baz = new Foo() }); // exception message points to "Baz.NonNullable" property
```

Additionally, you may use the `NotNullAttribute` to mark a nullable property as something
that should not return a null value. The NullabilityValidator will invalidate properties
violating this annotation as well. This even works on nullable value types!

```c#
#nullable enable

public class Foo {
    [NotNull] public int? Bar { get; init; }
}

// Valid - no exception thrown
NullabilityValidator.ValidateProperties(new Foo { Bar = 42 });

// Invalid - throws NonNullablePropertyIsNullException
NullabilityValidator.ValidateProperties(new Foo()); // exception message points to "Bar" property
```
