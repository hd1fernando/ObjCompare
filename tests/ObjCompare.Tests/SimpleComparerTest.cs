using FluentAssertions;
using Xunit;

namespace ObjCompare.Tests
{
    public class SimpleComparerTest
    {
        public class FakeClass
        {
            public int Value { get; set; }
        }

        [Fact(DisplayName = "Should compare an object with only one property")]
        public void ShouldCompareAnObjectWithOnlyOneProperty()
        {
            // Arrange
            FakeClass fakeOld = new() { Value = 10 };
            FakeClass fakeNew = new() { Value = 20 };
            SimpleComparer simple = new();

            // Act
            string result = simple.Compare(fakeOld, fakeNew);

            // Assert
            result.Should().BeEquivalentTo("Value:10,20");
        }
    }
}
