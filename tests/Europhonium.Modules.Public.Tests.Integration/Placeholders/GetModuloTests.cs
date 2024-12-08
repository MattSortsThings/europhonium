using Europhonium.Modules.Public.Placeholders;
using Europhonium.Modules.Public.Tests.Integration.Utils;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Modules.Public.Tests.Integration.Placeholders;

public static class GetModuloTests
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
        [InlineData(10, 1, 0)]
        [InlineData(5, 2, 1)]
        [InlineData(11, 4, 3)]
        public async Task ExecuteAsync_ValidParams_ReturnsExpectedRemainder(int dividend, int modulus, int remainder)
        {
            // Arrange
            GetModulo.Response expectedResponse = new(new ModuloResource
            {
                Dividend = dividend, Modulus = modulus, Remainder = remainder
            });

            // Act
            Results<Ok<GetModulo.Response>, ProblemHttpResult> result = await GetModulo.ExecuteAsync(dividend, modulus, Sender);

            // Assert
            using (new AssertionScope())
            {
                result.Result.Should().BeAssignableTo<Ok<GetModulo.Response>>()
                    .Which.StatusCode.Should().Be(200);

                result.Result.Should().BeAssignableTo<Ok<GetModulo.Response>>()
                    .Which.Value.Should().BeEquivalentTo(expectedResponse);
            }
        }
    }
}
