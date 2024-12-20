using System.Net;
using Europhonium.Apis.Admin.V1.Placeholders.Greetings;
using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class AdminApiV1HttpRequestSteps(ScenarioContext scenarioContext, IWebApiDriver webApiDriver)
{
    [When(@"I request (\d) greetings in (\w+)")]
    public async Task WhenIRequestDGreetingsInW(int quantity, LanguageOption language)
    {
        var route = $"placeholders/greetings?quantity={quantity}&language={language}";

        (HttpStatusCode statusCode, var content) = await webApiDriver.GETAsync(route);

        scenarioContext.AddResponseStatusCode(statusCode);
        scenarioContext.AddResponseContent(content);
    }
}
