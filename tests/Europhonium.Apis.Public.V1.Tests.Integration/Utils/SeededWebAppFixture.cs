using Europhonium.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace Europhonium.Apis.Public.V1.Tests.Integration.Utils;

public sealed class SeededWebAppFixture : WebApplicationFactory<IWebAppAssemblyLocator>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        // Seed database
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }
}
