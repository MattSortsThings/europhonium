using System.Net;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class HttpResponseSteps(ScenarioContext scenarioContext)
{
    [Then("""
          the response status code should be "(.*)"
          """)]
    public void ThenTheResponseStatusCodeShouldBe(HttpStatusCode expectedStatusCode) =>
        scenarioContext.Get<HttpStatusCode>().Should().Be(expectedStatusCode);
}
