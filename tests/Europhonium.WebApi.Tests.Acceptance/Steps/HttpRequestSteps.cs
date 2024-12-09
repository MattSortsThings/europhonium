using System.Net;
using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class HttpRequestSteps(ScenarioContext scenarioContext, IHttpClientProvider httpClientProvider)
{
    private HttpClient HttpClient { get; set; } = null!;

    [Given("I am a client using no API key")]
    public void GivenIAmAClientUsingNoApiKey() => HttpClient = httpClientProvider.GetClientUsingNoApiKey();

    [Given("I am a client using the Public API key")]
    public void GivenIAmAClientUsingThePublicApiKey() => HttpClient = httpClientProvider.GetClientUsingPublicApiKey();

    [Given("I am a client using the Admin API key")]
    public void GivenIAmAClientUsingTheAdminApiKey() => HttpClient = httpClientProvider.GetClientUsingAdminApiKey();

    [Given("I am a client using an unrecognized API key")]
    public void GivenIAmAClientUsingAnUnrecognizedApiKey() => HttpClient = httpClientProvider.GetClientUsingUnrecognizedApiKey();

    [When("I request (.*) mod (.*)")]
    public async Task WhenIRequestMod(int dividend, int modulus)
    {
        var route = $"api/v1/public/placeholders/modulo/{dividend}/{modulus}";

        (HttpStatusCode statusCode, var content) = await HttpClient.GETAsync(route);

        scenarioContext.Add(ScenarioKeys.HttpResponse.StatusCode, statusCode);
        scenarioContext.Add(ScenarioKeys.HttpResponse.Content, content);
    }

    [When(@"I create the following country")]
    public async Task WhenICreateTheFollowingCountry(string json)
    {
        const string route = "api/v1/admin/countries";

        (HttpStatusCode statusCode, var content, var location) = await HttpClient.POSTAsync(route, json);

        scenarioContext.Add(ScenarioKeys.HttpResponse.StatusCode, statusCode);
        scenarioContext.Add(ScenarioKeys.HttpResponse.Content, content);
        scenarioContext.Add(ScenarioKeys.HttpResponse.Location, location);
    }
}
