using Europhonium.WebApi.Tests.Acceptance.Drivers;
using Microsoft.AspNetCore.Mvc.Testing;
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
}
