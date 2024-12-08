namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public interface IHttpClientProvider
{
    public HttpClient GetClientUsingNoApiKey();
}
