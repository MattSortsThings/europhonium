using ErrorOr;
using Europhonium.Shared.Domain.Broadcasts;
using Europhonium.Shared.Domain.Contests;
using Europhonium.Shared.Domain.Countries;
using Europhonium.Shared.Domain.Tests.Unit.Utils;
using FluentAssertions.Execution;

namespace Europhonium.Shared.Domain.Tests.Unit.Countries;

public static class CountryTests
{
    private static readonly Guid[] SampleGuids =
    [
        Guid.Parse("6b091f38-a7c4-4a3c-b97d-ee9f5bbed19c"),
        Guid.Parse("48afe393-f7bc-4a87-8cf8-e08b65e0dc98"),
        Guid.Parse("e3ecd677-431a-4bff-ac0b-b3c5dd3117c3"),
        Guid.Parse("893c817f-7486-4a9e-9ad1-5901063f3ef4")
    ];

    [UnitTest]
    public sealed class ParticipatingContestIdsProperty
    {
        [Fact]
        public void ParticipatingContestIds_ReturnsCollectionSortedInAscendingOrder()
        {
            // Arrange
            Country sut = Country.Create().WithCountryCode("GB").AndName("United Kingdom").Build().Value;

            foreach (ContestId contestId in SampleGuids.Select(ContestId.FromValue))
            {
                sut.AddContestId(contestId);
            }

            // Act
            IReadOnlyCollection<ContestId> result = sut.ParticipatingContestIds;

            // Assert
            result.Should().NotBeEmpty().And.BeInAscendingOrder();
        }
    }

    [UnitTest]
    public sealed class CompetingBroadcastIdsProperty
    {
        [Fact]
        public void CompetingBroadcastIds_ReturnsCollectionSortedInAscendingOrder()
        {
            // Arrange
            Country sut = Country.Create().WithCountryCode("GB").AndName("United Kingdom").Build().Value;

            foreach (BroadcastId contestId in SampleGuids.Select(BroadcastId.FromValue))
            {
                sut.AddBroadcastId(contestId);
            }

            // Act
            IReadOnlyCollection<BroadcastId> result = sut.CompetingBroadcastIds;

            // Assert
            result.Should().NotBeEmpty().And.BeInAscendingOrder();
        }
    }

    [UnitTest]
    public sealed class VotingBroadcastIdsProperty
    {
        [Fact]
        public void VotingBroadcastIds_ReturnsCollectionSortedInAscendingOrder()
        {
            // Arrange
            Country sut = Country.Create().WithCountryCode("GB").AndName("United Kingdom").Build().Value;

            foreach (BroadcastId contestId in SampleGuids.Select(BroadcastId.FromValue))
            {
                sut.AddBroadcastId(contestId);
            }

            // Act
            IReadOnlyCollection<BroadcastId> result = sut.VotingBroadcastIds;

            // Assert
            result.Should().NotBeEmpty().And.BeInAscendingOrder();
        }
    }

    [UnitTest]
    public sealed class FluentBuilder
    {
        [Theory]
        [InlineData("GB", "United Kingdom")]
        [InlineData("CH", "Switzerland")]
        [InlineData("BA", "Bosnia & Herzegovina")]
        [InlineData("XX", "Rest of World")]
        [InlineData("IT", "Italy")]
        public void FluentBuilder_ValidArgs_ReturnsInstance(string countryCode, string name)
        {
            // Act
            ErrorOr<Country> result = Country.Create()
                .WithCountryCode(countryCode)
                .AndName(name)
                .Build();

            // Assert
            Country createdCountry = result.Value;

            using (new AssertionScope())
            {
                result.IsError.Should().BeFalse();

                createdCountry.CountryCode.Value.Should().Be(countryCode);
                createdCountry.Name.Should().Be(name);
                createdCountry.ParticipatingContestIds.Should().BeEmpty();
                createdCountry.CompetingBroadcastIds.Should().BeEmpty();
                createdCountry.VotingBroadcastIds.Should().BeEmpty();
            }
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AAA")]
        public void FluentBuilder_CountryCodeValueIsNotStringOf2UpperCaseLetters_ReturnsError(string countryCode)
        {
            // Act
            ErrorOr<Country> result = Country.Create()
                .WithCountryCode(countryCode)
                .AndName("NAME")
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.IsError.Should().BeTrue();
                result.FirstError.Should().HaveType(ErrorType.Validation)
                    .And.HaveCode("Countries.InvalidCountryCode")
                    .And.ContainMetadataKeyValuePair("countryCode", countryCode);
            }
        }

        [Fact]
        public void FluentBuilder_CountryCodeArgIsNull_Throws()
        {
            // Act
            Action act = () => Country.Create()
                .WithCountryCode(null!)
                .AndName("NAME")
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FluentBuilder_NameArgIsNull_Throws()
        {
            // Act
            Action act = () => Country.Create()
                .WithCountryCode("XX")
                .AndName(null!)
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
