using Europhonium.WebApi.Security;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public sealed partial class CleanWebAppFixture : IHttpClientProvider
{
    private const string AdminApiKey = "ADMIN_API_KEY";
    private const string PublicApiKey = "PUBLIC_API_KEY";
    private const string UnrecognizedApiKey = "CECI_N'EST_PAS_UNE_CLEF";


    private HttpClient? _clientUsingAdminApiKey;
    private HttpClient? _clientUsingNoApiKey;
    private HttpClient? _clientUsingPublicApiKey;
    private HttpClient? _clientUsingUnrecognizedApiKey;

    public HttpClient GetClientUsingNoApiKey() =>
        _clientUsingNoApiKey ??= CreateClientUsingNoApiKey();

    public HttpClient GetClientUsingAdminApiKey() =>
        _clientUsingAdminApiKey ??= CreateClientUsingAdminApiKey();

    public HttpClient GetClientUsingPublicApiKey() =>
        _clientUsingPublicApiKey ??= CreateClientUsingPublicApiKey();

    public HttpClient GetClientUsingUnrecognizedApiKey() =>
        _clientUsingUnrecognizedApiKey ??= CreateClientUsingUnrecognizedApiKey();

    private HttpClient CreateClientUsingNoApiKey() =>
        CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });

    private HttpClient CreateClientUsingAdminApiKey()
    {
        HttpClient client = CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });

        client.DefaultRequestHeaders.Add(ApiKeySecurityConstants.HttpRequestHeaderName, AdminApiKey);

        return client;
    }

    private HttpClient CreateClientUsingPublicApiKey()
    {
        HttpClient client = CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });

        client.DefaultRequestHeaders.Add(ApiKeySecurityConstants.HttpRequestHeaderName, PublicApiKey);

        return client;
    }

    private HttpClient CreateClientUsingUnrecognizedApiKey()
    {
        HttpClient client = CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });

        client.DefaultRequestHeaders.Add(ApiKeySecurityConstants.HttpRequestHeaderName, UnrecognizedApiKey);

        return client;
    }
}
