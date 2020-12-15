using FluentAssertions;
using System;
using Xunit;

namespace ObjCompare.Tests
{
    public class SimpleComparerTest
    {
        public class FakeClass
        {
            public int Value { get; set; }
        }

        public class AnotherFakeClass
        {
            public int Value { get; set; }
        }


        [Fact(DisplayName = "Should compare an object with only one property")]
        [Trait("Unit", "Simple Comparer")]
        public void ShouldCompareAnObjectWithOnlyOneProperty()
        {
            // Arrange
            FakeClass fakeOld = new() { Value = 10 };
            FakeClass fakeNew = new() { Value = 20 };
            SimpleComparer simple = new();

            // Act
            string result = simple.Compare(fakeOld, fakeNew);

            // Assert
            result.Should().BeEquivalentTo($"Value:10,20;");
        }

        [Fact(DisplayName = "Shoud return empty when no change happen")]
        [Trait("Unit", "Simple Comparer")]

        public void ShoudReturnEmptyWhenNoChangeHappen()
        {
            // Arrange
            FakeClass fakeOld = new() { Value = 10 };
            FakeClass fakeNew = new() { Value = 10 };
            SimpleComparer simple = new();

            // Act
            string result = simple.Compare(fakeOld, fakeNew);

            // Assert
            result.Should().BeEquivalentTo("");
        }

        [Fact(DisplayName = "Should throw an exception when the object is a different type")]
        [Trait("Unit", "Simple Comparer")]
        public void ShouldThrowAnExceptionWhenTheObjectIsADifferentType()
        {
            // Arrange
            FakeClass fakeOld = new() { Value = 10 };
            AnotherFakeClass fakeNew = new() { Value = 20 };
            SimpleComparer simple = new();

            // Act
            Action result = () => simple.Compare(fakeOld, fakeNew);

            // Assert
            result.Should().Throw<Exception>();
        }
    }
}
