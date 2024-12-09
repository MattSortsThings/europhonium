using ErrorOr;
using Europhonium.Shared.Domain.Countries;
using Europhonium.Shared.Domain.Tests.Unit.Utils;
using FluentAssertions.Execution;

namespace Europhonium.Shared.Domain.Tests.Unit.Countries;

public static class CountryCodeTests
{
    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceGivenItselfAsOther_ReturnsZero()
        {
            // Arrange
            CountryCode sut = CountryCode.FromValue("GB").Value;

            // Act
            var result = sut.CompareTo(sut);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceValuePrecedesOtherValue_ReturnsNegativeValue()
        {
            // Arrange
            CountryCode sut = CountryCode.FromValue("GB").Value;
            CountryCode other = CountryCode.FromValue("XX").Value;

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceValueFollowsOtherValue_ReturnsPositiveValue()
        {
            // Arrange
            CountryCode sut = CountryCode.FromValue("GB").Value;
            CountryCode other = CountryCode.FromValue("AM").Value;

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_OtherArgIsNull_ReturnsPositiveValue()
        {
            // Arrange
            CountryCode sut = CountryCode.FromValue("GB").Value;

            // Act
            var result = sut.CompareTo(null);

            // Assert
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            CountryCode sut = CountryCode.FromValue("GB").Value;

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalValues_ReturnsFalse()
        {
            // Arrange
            CountryCode sut = CountryCode.FromValue("GB").Value;
            CountryCode other = CountryCode.FromValue("XX").Value;

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            CountryCode sut = CountryCode.FromValue("GB").Value;

            // Act
            var result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class FromValueStaticMethod
    {
        [Theory]
        [InlineData("AA")]
        [InlineData("ZZ")]
        public void FromValue_ValueArgIsStringOf2UpperCaseLetters_ReturnsInstanceWithValue(string value)
        {
            // Act
            ErrorOr<CountryCode> result = CountryCode.FromValue(value);

            // Assert
            using (new AssertionScope())
            {
                result.IsError.Should().BeFalse();
                result.Value.Should().BeOfType<CountryCode>()
                    .Which.Value.Should().Be(value);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("aa")]
        [InlineData("A1")]
        [InlineData("A-")]
        [InlineData("  ")]
        [InlineData("AAA")]
        public void FromValue_ValueArgIsNotStringOf2UpperCaseLetters_ReturnsError(string value)
        {
            // Act
            ErrorOr<CountryCode> result = CountryCode.FromValue(value);

            // Assert
            using (new AssertionScope())
            {
                result.IsError.Should().BeTrue();
                result.FirstError.Should().HaveType(ErrorType.Validation)
                    .And.HaveCode("Countries.InvalidCountryCode")
                    .And.ContainMetadataKeyValuePair("countryCode", value);
            }
        }

        [Fact]
        public void FromValue_ValueArgIsNull_Throws()
        {
            // Act
            Action act = () => CountryCode.FromValue(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
