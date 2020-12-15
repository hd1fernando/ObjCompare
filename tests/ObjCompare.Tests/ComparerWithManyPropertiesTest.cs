using FluentAssertions;
using System;
using Xunit;

namespace ObjCompare.Tests
{
    public class ComparerWithManyPropertiesTest
    {
        public class FakeClass
        {
            public byte @Byte { get; set; }
            public int @Int { get; set; }
            public char @Char { get; set; }
            public string @String { get; set; }
            public decimal @Decimal { get; set; }
            public double @Double { get; set; }
            public float @Float { get; set; }
            public FakeEnum FakeEnum { get; set; }
            public ulong @Ulong { get; set; }
            public bool @Bool { get; set; }
        }

        public enum FakeEnum
        {
            FakeValue = 0,
            AnotherFakeValue = 1
        }

        public class ReferenceTypeClass
        {
            public int[] IntArray { get; set; }
        }


        [Fact(DisplayName = "Should compare with all propertyfilled")]
        [Trait("Unit", "With Many Properties")]
        public void ShouldCompareWithAllPropertyFilled()
        {
            // Arrange
            FakeClass oldFake = new() { Byte = 0x5, Char = 'a', Decimal = 25.0m, Double = 42.0, Float = 42.0f, Int = 42, String = "value", FakeEnum = FakeEnum.FakeValue, Ulong = 1000000, Bool = true };
            FakeClass newFake = new() { Byte = 0xA, Char = 'b', Decimal = 30.0m, Double = 52.0, Float = 52.0f, Int = 52, String = "another", FakeEnum = FakeEnum.AnotherFakeValue, Ulong = 10000000, Bool = false };
            SimpleComparer comparer = new();

            // Act
            var result = comparer.Compare(oldFake, newFake);

            // Assert
            result.Should()
                .BeEquivalentTo("Byte:5,10;Int:42,52;Char:a,b;String:value,another;Decimal:25.0,30.0;Double:42,52;Float:42,52;FakeEnum:FakeValue,AnotherFakeValue;Ulong:1000000,10000000;Bool:True,False;");

        }

        [Fact(DisplayName = "Should compare with all propertyfilled reference types")]
        [Trait("Unit", "With Many Properties")]
        public void ShouldCompareWithAllPropertyFilledReferenceType()
        {
            // Arrange
            ReferenceTypeClass oldFake = new() { IntArray = new int[] { 1 } };
            ReferenceTypeClass newFake = new() { IntArray = new int[] { 1, 2, 3 } };
            SimpleComparer comparer = new();

            // Act
            var result = comparer.Compare(oldFake, newFake);

            // Assert
            result.Should()
                .BeEquivalentTo("IntArray:[1],[1,2,3];");

        }

        [Fact(DisplayName = "Should compare with no all propertyfilled")]
        [Trait("Unit", "With Many Properties")]
        public void ShouldCompareWithNoAllPropertyFilled()
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
