using System.Net;
using Europhonium.Modules.Admin.Placeholders;
using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class HttpRequestSteps(ScenarioContext scenarioContext, IHttpClientProvider httpClientProvider)
{
    private HttpClient HttpClient { get; set; } = null!;

    [Given("I am a client using no API key")]
    public void GivenIAmAClientUsingNoApiKey() => HttpClient = httpClientProvider.GetClientUsingNoApiKey();

    [When("I request (.*) greetings in (.*)")]
    public async Task WhenIRequestGreetingsIn(int quantity, Language language)
    {
        var route = $"api/v1/admin/placeholders/greetings?quantity={quantity}&language={language}";

        (HttpStatusCode statusCode, var content) = await HttpClient.GETAsync(route);

        scenarioContext.Add(ScenarioKeys.HttpResponse.StatusCode, statusCode);
        scenarioContext.Add(ScenarioKeys.HttpResponse.Content, content);
    }

    [When("I request (.*) mod (.*)")]
    public async Task WhenIRequestMod(int dividend, int modulus)
    {
        var route = $"api/v1/public/placeholders/modulo/{dividend}/{modulus}";

        (HttpStatusCode statusCode, var content) = await HttpClient.GETAsync(route);

        scenarioContext.Add(ScenarioKeys.HttpResponse.StatusCode, statusCode);
        scenarioContext.Add(ScenarioKeys.HttpResponse.Content, content);
    }
}
