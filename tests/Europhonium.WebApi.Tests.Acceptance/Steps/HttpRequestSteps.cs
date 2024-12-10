using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;

namespace Europhonium.WebApi.Tests.Acceptance.Steps;

[Binding]
public sealed partial class HttpRequestSteps(ScenarioContext scenarioContext, IHttpClientProvider httpClientProvider)
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
}
