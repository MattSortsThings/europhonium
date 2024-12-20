using System.Net;
using Europhonium.WebApi.Tests.Acceptance.Utils;
using Humanizer;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class CommonHttpResponseSteps(
    FeatureContext featureContext,
    ScenarioContext scenarioContext,
    VerifySettings verifySettings)
{
    [Then("""
          the response status code should be "(\w+)"
          """)]
    public void ThenTheResponseStatusCodeShouldBe(HttpStatusCode expectedStatusCode)
    {
        scenarioContext.GetResponseStatusCode().Should().Be(expectedStatusCode);
    }

    [Then("the response content should match expectations")]
    public async Task ThenTheResponseContentShouldMatchExpectations()
    {
        var responseContent = scenarioContext.GetResponseContent();

        await VerifyJson(responseContent, verifySettings)
            .UseDirectory(GetFeatureFolderPathAsExpectationsDirectory())
            .UseTypeName(GetFeatureTitleAsTypeName())
            .UseMethodName(GetScenarioTitleAsMethodName());
    }

    private string GetFeatureFolderPathAsExpectationsDirectory() =>
        "Expectations/" + featureContext.FeatureInfo.FolderPath.Replace("Features/", string.Empty);

    private string GetFeatureTitleAsTypeName() => featureContext.FeatureInfo.Title
        .Transform(To.TitleCase)
        .Replace(" ", string.Empty)
        .Truncate(75);

    private string GetScenarioTitleAsMethodName() => scenarioContext.ScenarioInfo.Title
        .Transform(To.TitleCase)
        .Replace(" ", string.Empty)
        .Truncate(75);
}
