using _7Factor.Validation.Samples;
using Xunit;

namespace _7Factor.Validation;

public class NullabilityValidatorTest
{
    [Fact]
    public void ValidatePropertyReferences_NonNullablePropertyIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NonNullablePropertyClass { Property = "" });
    }

    [Fact]
    public void ValidatePropertyReferences_NonNullablePropertyIsNull_IsNotValid()
    {
        Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new NonNullablePropertyClass()));
    }

    [Fact]
    public void ValidatePropertyReferences_NonNullablePropertyIsNull_IsNotValidExceptionContainsPropName()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new NonNullablePropertyClass()));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Property"), e.Message);
    }

    [Fact]
    public void ValidatePropertyReferences_NullablePropertyIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NullablePropertyClass { Property = "" });
    }

    [Fact]
    public void ValidatePropertyReferences_NullablePropertyIsNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NullablePropertyClass());
    }

    [Fact]
    public void ValidatePropertyReferences_NullableDisabledPropertyIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NullableDisabledPropertyClass { Property = "" });
    }

    [Fact]
    public void ValidatePropertyReferences_NullableDisabledPropertyIsNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NullableDisabledPropertyClass());
    }

    [Fact]
    public void ValidatePropertyReferences_NonNullablePropertyOnRecordIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NonNullablePropertyRecord { Property = "" });
    }

    [Fact]
    public void ValidatePropertyReferences_NonNullablePropertyOnRecordIsNull_IsNotValid()
    {
        Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new NonNullablePropertyRecord()));
    }

    [Fact]
    public void ValidatePropertyReferences_TwoNonNullablePropertiesBothNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new TwoNonNullablePropertiesClass { Prop1 = "", Prop2 = "" });
    }

    [Fact]
    public void
        ValidatePropertyReferences_TwoNonNullableProperties1stPropIsNull_IsNotValidExceptionContains1stPropName()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new TwoNonNullablePropertiesClass { Prop2 = "" }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Prop1"), e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferences_TwoNonNullableProperties2ndPropIsNull_IsNotValidExceptionContains2ndPropName()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new TwoNonNullablePropertiesClass { Prop1 = "" }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Prop2"), e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferences_TwoNonNullablePropertiesBothPropsNull_IsNotValid()
    {
        Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new TwoNonNullablePropertiesClass()));
        // Cannot assert which prop causes exception since order of props through reflection is indeterminate.
    }

    [Fact]
    public void ValidatePropertyReferences_MixedNullabilityPropertiesBothNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new MixedNullabilityPropertiesClass
            { NonNullableProp = "", NullableProp = "" });
    }

    [Fact]
    public void ValidatePropertyReferences_MixedNullabilityPropertiesNullablePropIsNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new MixedNullabilityPropertiesClass { NonNullableProp = "" });
    }

    [Fact]
    public void ValidatePropertyReferences_MixedNullabilityPropertiesNonNullablePropIsNull_IsNotValid()
    {
        Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new MixedNullabilityPropertiesClass { NullableProp = "" }));
    }

    [Fact]
    public void ValidatePropertyReferences_MixedNullabilityPropertiesBothPropAreNull_IsNotValid()
    {
        Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new MixedNullabilityPropertiesClass()));
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnComplexClass_NonNullableChildIsNotNullAndNonNullablePropertyIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NonNullableChildWithNonNullablePropertyClass
            { Child = new NonNullablePropertyClass { Property = "" } });
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnComplexClass_NonNullableChildIsNotNullAndNonNullablePropertyIsNull_IsNotValidExceptionContainsSubPropertyName()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new NonNullableChildWithNonNullablePropertyClass
                { Child = new NonNullablePropertyClass() }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Child.Property"), e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnComplexClass_NonNullableChildIsNull_IsNotValidExceptionContainsSubPropertyName()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new NonNullableChildWithNonNullablePropertyClass()));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Child"), e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnComplexClass_NonNullableChildIsNotNullAndNullablePropertyIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NonNullableChildWithNullablePropertyClass
            { Child = new NullablePropertyClass { Property = "" } });
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnComplexClass_NonNullableChildIsNotNullAndNullablePropertyIsNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NonNullableChildWithNullablePropertyClass
            { Child = new NullablePropertyClass() });
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnComplexClass_NullableChildIsNotNullAndNonNullablePropertyIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NullableChildWithNonNullablePropertyClass
            { Child = new NonNullablePropertyClass { Property = "" } });
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnComplexClass_NullableChildIsNotNullAndNonNullablePropertyIsNull_IsNotValidExceptionContainsSubPropertyName()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new NullableChildWithNonNullablePropertyClass
                { Child = new NonNullablePropertyClass() }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Child.Property"), e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnComplexClass_NullableChildIsNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NullableChildWithNonNullablePropertyClass());
    }

    [Fact]
    public void ValidatePropertyReferences_ProtectedNonNullablePropertyIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new ProtectedNonNullablePropertyClass(""));
    }

    [Fact]
    public void ValidatePropertyReferences_ProtectedNonNullablePropertyIsNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new ProtectedNonNullablePropertyClass());
    }

    [Fact]
    public void ValidatePropertyReferences_PrivateNonNullablePropertyIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new PrivateNonNullablePropertyClass(""));
    }

    [Fact]
    public void ValidatePropertyReferences_PrivateNonNullablePropertyIsNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new PrivateNonNullablePropertyClass());
    }

    [Fact]
    public void ValidatePropertyReferencesOnEnumerableProp_ListWithNonNullablePropertyElementsEmpty_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new EnumerableNonNullablePropertyClass
            { Elements = new List<NonNullablePropertyClass>() });
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_ListWithNonNullablePropertyElementsWithOneValidElement_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new EnumerableNonNullablePropertyClass
            { Elements = new List<NonNullablePropertyClass> { new() { Property = "" } } });
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_ListWithNonNullablePropertyElementsWithOneNotValidElement_IsNotValidExceptionPointsTo1stElementProperty()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new EnumerableNonNullablePropertyClass
                { Elements = new List<NonNullablePropertyClass> { new() } }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Elements[0].Property"),
            e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_ListWithNonNullablePropertyElementsWith2ndElementNotValid_IsNotValidExceptionPointsTo2ndElementProperty()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new EnumerableNonNullablePropertyClass
                { Elements = new List<NonNullablePropertyClass> { new() { Property = "" }, new() } }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Elements[1].Property"),
            e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_SetWithNonNullablePropertyElementsWithOneNotValidElement_IsNotValidExceptionPointsToNonSpecificElementProperty()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new EnumerableNonNullablePropertyClass
                { Elements = new HashSet<NonNullablePropertyClass> { new() } }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Elements[].Property"),
            e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_ListWithNullablePropertyElementsWithOneElementPropertyIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new EnumerableNullablePropertyClass
            { Elements = new List<NullablePropertyClass> { new() { Property = "" } } });
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_ListWithNullablePropertyElementsWithOneElementPropertyIsNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new EnumerableNullablePropertyClass
            { Elements = new List<NullablePropertyClass> { new() } });
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_ListWithNullablePropertyElementsWithTwoElementsBothWherePropertyIsNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new EnumerableNullablePropertyClass
            { Elements = new List<NullablePropertyClass> { new(), new() } });
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_ListWithNonNullablePropertyElementsWithOneNullElement_IsNotValidExceptionPointsTo1stElement()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new EnumerableNonNullablePropertyClass
                { Elements = new List<NonNullablePropertyClass> { null } }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Elements[0]"),
            e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_ListWithNonNullablePropertyElementsWhere2ndElementIsNull_IsNotValidExceptionPointsTo2ndElement()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new EnumerableNonNullablePropertyClass
                { Elements = new List<NonNullablePropertyClass> { new() { Property = "" }, null } }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Elements[1]"),
            e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_ListWithNonNullablePropertyElementsWhere1stElementPropertyIsNullAnd2ndElementIsNull_IsNotValidExceptionPointsTo1stElementProperty()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new EnumerableNonNullablePropertyClass
                { Elements = new List<NonNullablePropertyClass> { new(), null } }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Elements[0].Property"),
            e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_SetWithNonNullablePropertyElementsWithOneNullElement_IsNotValidExceptionPointsToNonSpecificElement()
    {
        var e = Assert.Throws<NonNullableReferenceIsNullException>(() =>
            NullabilityValidator.ValidatePropertyReferences(new EnumerableNonNullablePropertyClass
                { Elements = new HashSet<NonNullablePropertyClass> { null } }));
        Assert.Equal(string.Format(NonNullableReferenceIsNullException.MessageFormat, "Elements[]"),
            e.Message);
    }

    [Fact]
    public void
        ValidatePropertyReferencesOnEnumerableProp_ListWithNullableElementsWithOneElement_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new EnumerableNullableElementsClass
            { Elements = new List<NonNullablePropertyClass?> { null } });
    }

    [Fact]
    public void ValidatePropertyReferences_NonNullableValuePropertyIsNotNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NonNullableValuePropertyClass { Property = 1 });
    }

    [Fact]
    public void ValidatePropertyReferences_NonNullableValuePropertyIsNull_IsValid()
    {
        NullabilityValidator.ValidatePropertyReferences(new NonNullableValuePropertyClass());
    }
}
