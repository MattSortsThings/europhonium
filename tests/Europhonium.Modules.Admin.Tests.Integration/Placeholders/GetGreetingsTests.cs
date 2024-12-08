using Europhonium.Modules.Admin.Placeholders;
using Europhonium.Modules.Admin.Tests.Integration.Utils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Modules.Admin.Tests.Integration.Placeholders;

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

        public static TheoryData<GetGreetings.Request, string> TestData =>
            new()
            {
                {
                    new GetGreetings.Request
                    {
                        Quantity = 2,
                        Language = Language.English
                    },
                    "Hi!"
                },
                {
                    new GetGreetings.Request
                    {
                        Quantity = 3,
                        Language = Language.French
                    },
                    "Bonjour!"
                },
                {
                    new GetGreetings.Request
                    {
                        Quantity = 5,
                        Language = Language.Dutch
                    },
                    "Hoi!"
                }
            };

        [Theory]
        [MemberData(nameof(TestData), MemberType = typeof(ExecuteAsyncMethod))]
        public async Task ExecuteAsync_ValidRequest_ReturnsRequestedGreetings(GetGreetings.Request request,
            string expectedGreeting)
        {
            // Act
            Ok<GetGreetings.Response> result = await GetGreetings.ExecuteAsync(request, Sender);

            // Assert
            result.Value.Should().BeOfType<GetGreetings.Response>()
                .Which.Greetings.Should().HaveCount(request.Quantity)
                .And.AllSatisfy(resource => resource.Greeting.Should().Be(expectedGreeting))
                .And.AllSatisfy(resource => resource.Language.Should().Be(request.Language));
        }
    }
}
