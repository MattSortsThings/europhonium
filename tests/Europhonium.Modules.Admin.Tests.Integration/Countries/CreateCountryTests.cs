using Europhonium.Modules.Admin.Countries;
using Europhonium.Modules.Admin.Tests.Integration.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Modules.Admin.Tests.Integration.Countries;

public static class CreateCountryTests
{
    private static void IsCreatedFrom(this CountryResource country, CreateCountry.Request request)
    {
        country.CountryCode.Should().Be(request.CountryCode);
        country.CountryName.Should().Be(request.Name);
        country.ParticipatingContestIds.Should().BeEmpty();
        country.CompetingBroadcastIds.Should().BeEmpty();
        country.VotingBroadcastIds.Should().BeEmpty();
    }

    [Expensive]
    [DatabaseTest]
    [IntegrationTest]
    [Feature("A/1/1")]
    public sealed class ExecuteAsyncMethod : CleanWebAppTestBase
    {
        public ExecuteAsyncMethod(CleanWebAppFixture webAppFixture) : base(webAppFixture)
        {
        }

        [Theory]
        [HappyPath]
        [InlineData("CH", "Switzerland")]
        [InlineData("GB", "United Kingdom")]
        [InlineData("BA", "Bosnia & Herzegovina")]
        public async Task ExecuteAsync_ValidRequest_ReturnsOkResultWithCreatedCountry(string countryCode, string name)
        {
            // Arrange
            CreateCountry.Request request = new()
            {
                CountryCode = countryCode,
                Name = name
            };

            // Act
            Results<Ok<CreateCountry.Response>, ProblemHttpResult> result = await CreateCountry.ExecuteAsync(request, Sender);

            // Assert
            result.Result.Should().BeAssignableTo<Ok<CreateCountry.Response>>()
                .Which.Value.Should().BeOfType<CreateCountry.Response>()
                .Which.Country.IsCreatedFrom(request);
        }

        [Theory]
        [SadPath]
        [InlineData("12345")]
        [InlineData("INVALID_COUNTRY_CODE")]
        public async Task ExecuteAsync_InvalidCountryCode_ReturnsBadRequestResultWithProblemDetails(string countryCode)
        {
            // Arrange
            CreateCountry.Request request = new()
            {
                CountryCode = countryCode,
                Name = "Name"
            };

            // Act
            Results<Ok<CreateCountry.Response>, ProblemHttpResult> result = await CreateCountry.ExecuteAsync(request, Sender);

            // Assert
            result.Result.Should().BeAssignableTo<ProblemHttpResult>()
                .Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Theory]
        [SadPath]
        [InlineData("FR")]
        [InlineData("GB")]
        public async Task ExecuteAsync_CountryCodeConflict_ReturnsConflictResultWithProblemDetails(string countryCode)
        {
            // Arrange
            await CreateCountries("FR", "GB", "XX");

            CreateCountry.Request request = new()
            {
                CountryCode = countryCode,
                Name = "Name"
            };

            // Act
            Results<Ok<CreateCountry.Response>, ProblemHttpResult> result = await CreateCountry.ExecuteAsync(request, Sender);

            // Assert
            result.Result.Should().BeAssignableTo<ProblemHttpResult>()
                .Which.StatusCode.Should().Be(StatusCodes.Status409Conflict);
        }

        private async Task CreateCountries(params string[] countryCodes)
        {
            foreach (var countryCode in countryCodes)
            {
                CreateCountry.Request request = new()
                {
                    CountryCode = countryCode,
                    Name = "Name " + countryCode
                };
                _ = await CreateCountry.ExecuteAsync(request, Sender);
            }
        }
    }
}
