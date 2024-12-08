using Europhonium.Modules.Admin.Placeholders;
using Europhonium.Modules.Admin.Tests.Integration.Utils;
using FluentAssertions.Execution;
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

        public static TheoryData<GetGreetings.Request, GetGreetings.Response> TestData =>
            new()
            {
                {
                    new GetGreetings.Request
                    {
                        Quantity = 2,
                        Language = Language.English
                    },
                    new GetGreetings.Response([
                        new GreetingResource("Hi!", Language.English),
                        new GreetingResource("Hi!", Language.English)
                    ])
                },
                {
                    new GetGreetings.Request
                    {
                        Quantity = 3,
                        Language = Language.French
                    },
                    new GetGreetings.Response([
                        new GreetingResource("Bonjour!", Language.French),
                        new GreetingResource("Bonjour!", Language.French),
                        new GreetingResource("Bonjour!", Language.French)
                    ])
                }
            };

        [Theory]
        [MemberData(nameof(TestData), MemberType = typeof(ExecuteAsyncMethod))]
        public async Task ExecuteAsync_ValidRequest_ReturnsRequestedGreetings(GetGreetings.Request request,
            GetGreetings.Response response)
        {
            // Act
            Results<Ok<GetGreetings.Response>, ProblemHttpResult> result = await GetGreetings.ExecuteAsync(request, Sender);

            // Assert
            using (new AssertionScope())
            {
                result.Result.Should().BeAssignableTo<Ok<GetGreetings.Response>>()
                    .Which.StatusCode.Should().Be(200);

                result.Result.Should().BeAssignableTo<Ok<GetGreetings.Response>>()
                    .Which.Value.Should().BeEquivalentTo(response);
            }
        }
    }
}
