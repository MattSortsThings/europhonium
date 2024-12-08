using Europhonium.WebApi.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public sealed class CleanWebAppFixture : WebApplicationFactory<IWebApiAssemblyLocator>,
    IAsyncLifetime,
    IHttpClientProvider
{
    private const string AdminApiKey = "ADMIN_API_KEY";
    private const string PublicApiKey = "PUBLIC_API_KEY";
    private const string UnrecognizedApiKey = "CECI_N'EST_PAS_UNE_CLEF";
    private static readonly Uri BaseUri = new("http://localhost:5083");
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();
    private HttpClient? _clientUsingAdminApiKey;

    private HttpClient? _clientUsingNoApiKey;
    private HttpClient? _clientUsingPublicApiKey;
    private HttpClient? _clientUsingUnrecognizedApiKey;

    public Task InitializeAsync() => _dbContainer.StartAsync();

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }

    public HttpClient GetClientUsingNoApiKey() =>
        _clientUsingNoApiKey ??= CreateClientUsingNoApiKey();

    public HttpClient GetClientUsingAdminApiKey() =>
        _clientUsingAdminApiKey ??= CreateClientUsingAdminApiKey();

    public HttpClient GetClientUsingPublicApiKey() =>
        _clientUsingPublicApiKey ??= CreateClientUsingPublicApiKey();

    public HttpClient GetClientUsingUnrecognizedApiKey() =>
        _clientUsingUnrecognizedApiKey ??= CreateClientUsingUnrecognizedApiKey();

    internal void Reset()
    {
        // Reset database here.
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddOptions<ApiKeySecurityOptions>().Configure(a =>
            {
                a.AdminApiKey = AdminApiKey;
                a.PublicApiKey = PublicApiKey;
            });
        });
    }

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
