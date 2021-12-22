# DotnetValidation

A simple library for validating objects.

Currently this library supports validating that the public properties of an object are
not null if that property's type is a non-nullable reference type (in a `#nullable enable`
context.) This is sometimes needed to validate objects after they have been created via
reflection (such as from JSON deserialization.)


## Usage

First, you will need to add the NuGet package `7Factor.Validation` to your project. Then,
using the `_7Factor.Validation` namespace, simply pass the object you wish to validate to 
`NullabilityValidator.ValidatePropertyReferences()`. If the properties of the object violate 
their type's nullability then a `NonNullableReferenceIsNullException` will be thrown with a
message indicating the property that caused the validation to fail.
