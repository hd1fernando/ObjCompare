using FluentAssertions;
using Xunit;

namespace ObjCompare.Tests
{
    public class ComparerWithManyProperties
    {
        public class FakeClass
        {
            public int @Int { get; set; }
            public string @String { get; set; }
            public decimal @Decimal { get; set; }
            public double @Double { get; set; }
            public float @Float { get; set; }
        }

        [Fact(DisplayName = "")]
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
    }
}
