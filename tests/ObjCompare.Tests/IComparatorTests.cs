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

        [Theory(DisplayName = "")]
        [InlineData("string")]
        [InlineData(10)]
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

        [Theory(DisplayName = "")]
        [InlineData("vader")]
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

        [Theory(DisplayName = "")]
        [InlineData("vader")]
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
