using FluentAssertions;
using Xunit;

namespace ObjCompare.Tests
{
    public class IComparatorTests
    {
        public class FakeClasse
        {
            public object Value { get; set; }
        }

        [Theory(DisplayName = "PropertyValue - Should return PropertyValue")]
        [InlineData("string")]
        [InlineData(10)]
        [Trait("Unit", "PropertyValue")]
        public void PropertyValue_ShouldReturnPropertyValue(object value)
        {
            // Arrange
            FakeClasse fake = new() { Value = value };

            // Act
            IComparator property = new PropertyValue(fake.GetType().GetProperty("Value"), fake);
            var result = property.Validade();

            // Assert
            result.Should().Be(value);
        }

        [Fact(DisplayName = "PropertyName - Shoul return PropertyName")]
        [Trait("Unit", "PropertyName")]
        public void PropertyName_ShoulReturnPropertyName()
        {
            // Arrange
            FakeClasse fake = new() { Value = string.Empty };
            IComparator comparator = new PropertyName(fake.GetType().GetProperty(nameof(fake.Value)), fake);

            // Act
            var result = comparator.Validade();

            // Assert
            result.Should().Be(nameof(fake.Value));
        }

        [Theory(DisplayName = "PropertyComparator - Should return false when compare two properties values")]
        [InlineData("vader")]
        [Trait("Unit", "PropertyComparator")]
        public void PropertyComparator_ShouldReturnFalseWhenCompareTwoPropertiesValues(object expectedValue)
        {
            // Arrange
            FakeClasse fake = new() { Value = expectedValue };
            var fakeValue = fake.GetType().GetProperty(nameof(fake.Value));

            FakeClasse newFake = new() { Value = $"Marvin {expectedValue}" };
            var newFakeValue = newFake.GetType().GetProperty(nameof(newFake.Value));

            IComparator comparator = new PropertyComparator(new PropertyValue(fakeValue, fake), new PropertyValue(newFakeValue, newFake));

            // Act
            var result = (bool)comparator.Validade();

            // Assert
            result.Should().BeFalse();
        }

        [Theory(DisplayName = "PropertyComparator - Should return true when compare two properties values")]
        [InlineData("vader")]
        [Trait("Unit", "PropertyComparator")]
        public void PropertyComparator_ShouldReturnTrueWhenCompareTwoPropertiesValues(object expectedValue)
        {
            // Arrange
            FakeClasse fake = new() { Value = expectedValue };
            var value = fake.GetType().GetProperty(nameof(fake.Value));

            IComparator comparator = new PropertyComparator(new PropertyValue(value, fake), new PropertyValue(value, fake));

            // Act
            var result = (bool)comparator.Validade();

            // Assert
            result.Should().BeTrue();
        }
    }
}
