using System.Net;
using Humanizer;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class HttpResponseSteps(
    FeatureContext featureContext,
    ScenarioContext scenarioContext,
    VerifySettings verifySettings)
{
    [Then("""
          the response status code should be "(.*)"
          """)]
    public void ThenTheResponseStatusCodeShouldBe(HttpStatusCode expectedStatusCode)
    {
        scenarioContext.Get<HttpStatusCode>(ScenarioKeys.HttpResponse.StatusCode).Should().Be(expectedStatusCode);
    }

    [Then("the response content should match expectations")]
    public async Task ThenTheResponseContentShouldMatchExpectations()
    {
        var responseContent = scenarioContext.Get<string>(ScenarioKeys.HttpResponse.Content);

        await VerifyJson(responseContent, verifySettings)
            .UseDirectory("Expectations/" + featureContext.FeatureInfo.FolderPath)
            .UseTypeName(GetFeatureTitleAsTypeName())
            .UseMethodName(GetScenarioTitleAsMethodName());
    }

    private string GetFeatureTitleAsTypeName() => featureContext.FeatureInfo.Title
        .Transform(To.TitleCase)
        .Replace(" ", string.Empty)
        .Truncate(75);

    private string GetScenarioTitleAsMethodName() => scenarioContext.ScenarioInfo.Title
        .Transform(To.TitleCase)
        .Replace(" ", string.Empty)
        .Truncate(75);
}
