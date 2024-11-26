namespace Europhonium.WebApi.Tests.Acceptance.Drivers;

public interface IHttpClientProvider
{
    public HttpClient CreateClientUsingNoApiKey();
}
