using FluentAssertions;
using Xunit;

namespace ObjCompare.Tests
{
    public class IVisitorTest
    {
        public class FakeClass
        {
            public int Value { get; set; }
        }

        [Fact(DisplayName = "Should generate commun print.")]
        [Trait("Unit", "IVisitor")]
        public void ShoudGenerateCommunPrint()
        {
            // Arrange
            FakeClass fake = new() { Value = 42 };
            var propValue = fake.GetType().GetProperty(nameof(fake.Value));

            IComparator comparator1 = new PropertyValue(propValue, fake);
            IComparator comparator2 = new PropertyValue(propValue, fake);
            IComparator compare = new PropertyComparator(comparator1, comparator2);

            IVisitor print = new ComunPrint();

            // Act
            var result = compare.Accept(print);

            // Assert
            result.Should().BeEquivalentTo("42,42;");
        }
    }
}
