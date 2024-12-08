using Europhonium.Modules.Public.Placeholders;
using Europhonium.Modules.Public.Tests.Integration.Utils;
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
        public async Task ExecuteAsync_ValidParams_ReturnsExpectedRemainder(int dividend, int modulus, int expectedRemainder)
        {
            // Act
            Ok<GetModulo.Response> result = await GetModulo.ExecuteAsync(dividend, modulus, Sender);

            // Assert
            result.Value.Should().BeOfType<GetModulo.Response>()
                .Which.Modulo.Remainder.Should().Be(expectedRemainder);
        }
    }
}
