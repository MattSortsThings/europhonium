using Europhonium.Apis.Public.V1.Placeholders.ModuloCalculation;
using Europhonium.Apis.Public.V1.Tests.Integration.Utils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Apis.Public.V1.Tests.Integration.Placeholders.ModuloCalculations;

public static class GetModuloCalculationTests
{
    [Expensive]
    [DatabaseTest]
    [IntegrationTest]
    public sealed class ExecuteAsyncMethod : SeededWebAppTestBase
    {
        public ExecuteAsyncMethod(SeededWebAppFixture webAppFixture) : base(webAppFixture)
        {
        }

        [Theory]
        [InlineData(1, 1, 0)]
        [InlineData(10, 3, 1)]
        [InlineData(10, 4, 2)]
        public async Task ExecuteAsync_ValidRequest_ReturnsOkResult(int dividend, int modulus, int expectedRemainder)
        {
            // Act
            Results<Ok<GetModuloCalculation.Response>, BadRequest> result =
                await GetModuloCalculation.ExecuteAsync(dividend, modulus, Sender);

            // Assert
            result.Result.Should().BeAssignableTo<Ok<GetModuloCalculation.Response>>()
                .Which.Value.Should().BeOfType<GetModuloCalculation.Response>()
                .Which.ModuloCalculation.Remainder.Should().Be(expectedRemainder);
        }
    }
}
