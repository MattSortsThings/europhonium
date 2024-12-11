using System.Net;
using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

public sealed partial class HttpRequestSteps
{
    [When("I create the following country")]
    public async Task WhenICreateTheFollowingCountry(string json)
    {
        const string route = "api/v1/admin/countries";

        (HttpStatusCode statusCode, var content, var location) = await HttpClient.POSTAsync(route, json);

        scenarioContext.Add(ScenarioKeys.HttpResponse.StatusCode, statusCode);
        scenarioContext.Add(ScenarioKeys.HttpResponse.Content, content);
        scenarioContext.Add(ScenarioKeys.HttpResponse.Location, location);
    }

    [When("""I request the country with the country code "(.*)" by its ID""")]
    public async Task WhenIRequestTheCountryWithTheCountryCodeByItsId(string countryCode)
    {
        var id = scenarioContext.Get<Guid>(countryCode);

        var route = $"api/v1/admin/countries/{id}";

        (HttpStatusCode statusCode, var content) = await HttpClient.GETAsync(route);

        scenarioContext.Add(ScenarioKeys.HttpResponse.StatusCode, statusCode);
        scenarioContext.Add(ScenarioKeys.HttpResponse.Content, content);
    }

    [When("I request a country that does not exist")]
    public async Task WhenIRequestACountryThatDoesNotExist()
    {
        var route = "api/v1/admin/countries/" + Guid.NewGuid();

        (HttpStatusCode statusCode, var content) = await HttpClient.GETAsync(route);

        scenarioContext.Add(ScenarioKeys.HttpResponse.StatusCode, statusCode);
        scenarioContext.Add(ScenarioKeys.HttpResponse.Content, content);
    }
}
