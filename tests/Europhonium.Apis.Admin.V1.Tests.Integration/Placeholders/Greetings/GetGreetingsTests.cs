using Europhonium.Apis.Admin.V1.Placeholders.Greetings;
using Europhonium.Apis.Admin.V1.Tests.Integration.Utils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Apis.Admin.V1.Tests.Integration.Placeholders.Greetings;

public static class GetGreetingsTests
{
    [Expensive]
    [DatabaseTest]
    [IntegrationTest]
    public sealed class ExecuteAsyncMethod : CleanWebAppTestBase
    {
        public ExecuteAsyncMethod(CleanWebAppFixture webAppFixture) : base(webAppFixture)
        {
        }

        [Theory]
        [InlineData(1, LanguageOption.French, "Bonjour!")]
        [InlineData(10, LanguageOption.English, "Hi!")]
        [InlineData(7, LanguageOption.Dutch, "Hoi!")]
        public async Task ExecuteAsync_ValidRequest_ReturnsOkResult(int quantity, LanguageOption language, string expected)
        {
            // Arrange
            GetGreetings.Request request = new() { Quantity = quantity, Language = language };

            // Act
            Results<Ok<GetGreetings.Response>, BadRequest> result = await GetGreetings.ExecuteAsync(request, Sender);

            // Assert
            result.Result.Should().BeAssignableTo<Ok<GetGreetings.Response>>()
                .Which.Value.Should().BeOfType<GetGreetings.Response>()
                .Which.Greetings.Should().HaveCount(quantity)
                .And.AllSatisfy(dto => dto.Message.Should().Be(expected));
        }
    }
}
