using Europhonium.Modules.Public.Queryables;
using Europhonium.Modules.Public.Tests.Integration.Utils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Modules.Public.Tests.Integration.Queryables;

public static class GetQueryableCountriesTests
{
    [Expensive]
    [DatabaseTest]
    [IntegrationTest]
    [Feature("P/1/3")]
    public sealed class ExecuteAsyncMethod : SeededWebAppTestBase
    {
        public ExecuteAsyncMethod(SeededWebAppFixture webAppFixture) : base(webAppFixture)
        {
        }

        [Fact]
        [HappyPath]
        public async Task ExecuteAsync_ReturnsAllQueryableCountriesOrderedByCountryCode()
        {
            // Act
            Results<Ok<GetQueryableCountries.Response>, ProblemHttpResult> result =
                await GetQueryableCountries.ExecuteAsync(Sender);

            // Assert
            result.Result.Should().BeAssignableTo<Ok<GetQueryableCountries.Response>>()
                .Which.Value.Should().BeOfType<GetQueryableCountries.Response>()
                .Which.QueryableCountries.Should().BeInAscendingOrder(item => item.CountryCode)
                .And.SatisfyRespectively(resource0 =>
                {
                    resource0.CountryCode.Should().Be("AT");
                    resource0.Name.Should().Be("Austria");
                }, resource1 =>
                {
                    resource1.CountryCode.Should().Be("BE");
                    resource1.Name.Should().Be("Belgium");
                }, resource2 =>
                {
                    resource2.CountryCode.Should().Be("CZ");
                    resource2.Name.Should().Be("Czechia");
                }, resource3 =>
                {
                    resource3.CountryCode.Should().Be("DE");
                    resource3.Name.Should().Be("Germany");
                }, resource4 =>
                {
                    resource4.CountryCode.Should().Be("EE");
                    resource4.Name.Should().Be("Estonia");
                }, resource5 =>
                {
                    resource5.CountryCode.Should().Be("FI");
                    resource5.Name.Should().Be("Finland");
                }, resource6 =>
                {
                    resource6.CountryCode.Should().Be("GB");
                    resource6.Name.Should().Be("United Kingdom");
                }, resource7 =>
                {
                    resource7.CountryCode.Should().Be("HR");
                    resource7.Name.Should().Be("Croatia");
                }, resource8 =>
                {
                    resource8.CountryCode.Should().Be("XX");
                    resource8.Name.Should().Be("Rest of World");
                });
        }
    }
}
