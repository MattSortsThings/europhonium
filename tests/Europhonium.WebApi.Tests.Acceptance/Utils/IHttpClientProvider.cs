namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public interface IHttpClientProvider
{
    public HttpClient GetClientUsingNoApiKey();

    public HttpClient GetClientUsingAdminApiKey();

    public HttpClient GetClientUsingPublicApiKey();

    public HttpClient GetClientUsingUnrecognizedApiKey();
}
