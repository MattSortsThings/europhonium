namespace Europhonium.WebApi.Tests.Acceptance.Drivers;

public interface IHttpClientProvider
{
    public HttpClient CreateClientUsingNoApiKey();

    public HttpClient CreateClientUsingAdminApiKey();

    public HttpClient CreateClientUsingPublicApiKey();

    public HttpClient CreateClientUsingUnrecognizedApiKey();
}
