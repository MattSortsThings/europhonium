using Europhonium.WebApi.Tests.Acceptance.Drivers;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed class HttpRequestSteps(ScenarioContext scenarioContext, IHttpClientProvider clientProvider)
{
    private HttpClient Client { get; set; } = null!;

    [Given("I am a client using no API key")]
    public void GivenIAmAClientUsingNoApiKey() => Client = clientProvider.CreateClientUsingNoApiKey();

    [When("I request (.*) greetings")]
    public async Task WhenIRequestGreetings(int quantity)
    {
        var route = $"api/admin/placeholders/greetings?quantity={quantity}";

        HttpResponseMessage? response = await Client.GetAsync(route);

        scenarioContext.Set(response.StatusCode);
    }

    [When("I request (.*) mod (.*)")]
    public async Task WhenIRequestMod(int dividend, int modulus)
    {
        var route = $"api/public/placeholders/modulo/{dividend}/{modulus}";

        HttpResponseMessage? response = await Client.GetAsync(route);

        scenarioContext.Set(response.StatusCode);
    }
}
