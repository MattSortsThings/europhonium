using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public sealed class CleanWebAppFixture : WebApplicationFactory<IWebApiAssemblyLocator>,
    IAsyncLifetime,
    IHttpClientProvider
{
    private static readonly Uri BaseUri = new("http://localhost:5083");
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    private HttpClient? _clientUsingNoApiKey;

    public Task InitializeAsync() => _dbContainer.StartAsync();

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }

    public HttpClient GetClientUsingNoApiKey() =>
        _clientUsingNoApiKey ??= CreateClientUsingNoApiKey();

    internal void Reset()
    {
        // Reset database here.
    }

    private HttpClient CreateClientUsingNoApiKey() =>
        CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
}
