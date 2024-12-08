using Europhonium.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace Europhonium.Modules.Public.Tests.Integration.Utils;

public sealed class SeededWebAppFixture : WebApplicationFactory<IWebApiAssemblyLocator>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    public Task InitializeAsync() => _dbContainer.StartAsync();

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }
}
