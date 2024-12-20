using System.Net;
using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class PublicApiV1HttpRequestSteps(ScenarioContext scenarioContext, IWebApiDriver webApiDriver)
{
    [When(@"I request the calculation (\d+) mod (\d+)")]
    public async Task WhenIRequestTheCalculationDModD(int dividend, int modulus)
    {
        var route = $"placeholders/modulo-calculations/{dividend}/{modulus}";

        (HttpStatusCode statusCode, var content) = await webApiDriver.GETAsync(route);

        scenarioContext.AddResponseStatusCode(statusCode);
        scenarioContext.AddResponseContent(content);
    }
}
