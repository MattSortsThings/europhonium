using Europhonium.WebApi.Tests.Acceptance.Drivers;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class HttpRequestSteps(ScenarioContext scenarioContext, IHttpClientProvider clientProvider)
{
    private HttpClient Client { get; set; } = null!;

    [Given("I am a client using no API key")]
    public void GivenIAmAClientUsingNoApiKey() => Client = clientProvider.CreateClientUsingNoApiKey();

    [Given("I am a client using the Admin API key")]
    public void GivenIAmAClientUsingTheAdminApiKey() => Client = clientProvider.CreateClientUsingAdminApiKey();

    [Given("I am a client using the Public API key")]
    public void GivenIAmAClientUsingThePublicApiKey() => Client = clientProvider.CreateClientUsingPublicApiKey();

    [Given("I am a client using an unrecognized API key")]
    public void GivenIAmAClientUsingAnUnrecognizedApiKey() => Client = clientProvider.CreateClientUsingUnrecognizedApiKey();

    [When("I request (.*) greetings")]
    public async Task WhenIRequestGreetings(int quantity)
    {
        var route = $"api/admin/placeholders/greetings?quantity={quantity}";

        HttpResponseMessage response = await Client.GetAsync(route);

        scenarioContext.Set(response.StatusCode);
    }

    [When("I request (.*) mod (.*)")]
    public async Task WhenIRequestMod(int dividend, int modulus)
    {
        var route = $"api/public/placeholders/modulo/{dividend}/{modulus}";

        HttpResponseMessage response = await Client.GetAsync(route);

        scenarioContext.Set(response.StatusCode);
    }

    [When("I request the Swagger page")]
    public async Task WhenIRequestTheSwaggerPage()
    {
        const string route = "swagger";

        HttpResponseMessage response = await Client.GetAsync(route);

        scenarioContext.Set(response.StatusCode);
    }

    [When("""I request the "(.*)" Swagger document""")]
    public async Task WhenIRequestTheSwaggerDocument(string documentName)
    {
        var route = $"swagger/{documentName}/swagger.json";

        HttpResponseMessage response = await Client.GetAsync(route);

        scenarioContext.Set(response.StatusCode);
    }
}
