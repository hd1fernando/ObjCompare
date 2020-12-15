using FluentAssertions;
using System;
using Xunit;

namespace ObjCompare.Tests
{
    public class ComparerWithManyPropertiesTest
    {
        public class FakeClass
        {
            public int @Int { get; set; }
            public string @String { get; set; }
            public decimal @Decimal { get; set; }
            public double @Double { get; set; }
            public float @Float { get; set; }
        }

        [Fact(DisplayName = "Should compare with not all propertyfilled")]
        [Trait("Unit", "With Many Properties")]
        public void ShouldCompareWithNotAllPropertyFilled()
        {
            // Arrange
            FakeClass oldFake = new() { Int = 10, String = "Lorem" };
            FakeClass newFake = new() { Int = 10, String = "Valhala", @Decimal = 42.0m };
            SimpleComparer comparer = new();

            // Act
            var result = comparer.Compare(oldFake, newFake);

            // Assert
            result.Should().BeEquivalentTo(
                @"String:Lorem,Valhala;Decimal:0,42.0;");
        }


        [Fact(DisplayName = "Should compare with different order properties")]
        [Trait("Unit", "With Many Properties")]
        public void ShouldCompareWithDifferentOrderProperties()
        {
            // Arrange
            FakeClass oldFake = new() { Int = 10, String = "Lorem", @Decimal = 4.0m };
            FakeClass newFake = new() { @Decimal = 42.0m, Int = 10, String = "Valhala", };
            SimpleComparer comparer = new();

            // Act
            var result = comparer.Compare(oldFake, newFake);

            // Assert
            result.Should().BeEquivalentTo(
                @"String:Lorem,Valhala;Decimal:4.0,42.0;");
        }

        [Fact(DisplayName = "Should throw an exception when an object is null")]
        [Trait("Unit", "With Many Properties")]
        public void ShouldThrowAnExceptionWhenAnObjectIsNull()
        {
            // Arrange
            FakeClass oldFake = new() { Int = 10, String = "Lorem", @Decimal = 4.0m };

            FakeClass newFake = null;
            SimpleComparer comparer = new();

            // Act
            Action result = () => comparer.Compare(oldFake, newFake);

            // Assert
            result.Should().Throw<ArgumentNullException>()
                .WithMessage("Can't be null (Parameter 'newObject')");
        }
    }
}
