using Europhonium.Endpoints.Shared.Security;
using Europhonium.WebApi.Tests.Acceptance.Drivers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Testcontainers.MsSql;

namespace Europhonium.WebApi.Tests.Acceptance.Utils.Fixtures;

public sealed class WebAppFixture : WebApplicationFactory<IWebApiAssemblyLocator>, IAsyncLifetime, IHttpClientProvider
{
    private static readonly Uri BaseUri = new("http://localhost:5167");

    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    public Task InitializeAsync() => _dbContainer.StartAsync();

    public new Task DisposeAsync() => _dbContainer.DisposeAsync().AsTask();

    public HttpClient CreateClientUsingNoApiKey() =>
        CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });

    public HttpClient CreateClientUsingAdminApiKey()
    {
        HttpClient client = CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });

        var apiKey = Services.GetRequiredService<IOptionsMonitor<ApiKeyOptions>>().CurrentValue.AdminApiKey;

        client.DefaultRequestHeaders.Add(SecurityConstants.ApiKeyRequestHeaderName, apiKey);

        return client;
    }

    public HttpClient CreateClientUsingPublicApiKey()
    {
        HttpClient client = CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });

        var apiKey = Services.GetRequiredService<IOptionsMonitor<ApiKeyOptions>>().CurrentValue.PublicApiKey;

        client.DefaultRequestHeaders.Add(SecurityConstants.ApiKeyRequestHeaderName, apiKey);

        return client;
    }

    public HttpClient CreateClientUsingUnrecognizedApiKey()
    {
        HttpClient client = CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });

        client.DefaultRequestHeaders.Add(SecurityConstants.ApiKeyRequestHeaderName, "THIS_IS_NOT_AN_API_KEY!");

        return client;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json", false, true);
        });
    }
}
