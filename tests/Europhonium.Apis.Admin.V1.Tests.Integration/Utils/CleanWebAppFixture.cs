using Europhonium.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace Europhonium.Apis.Admin.V1.Tests.Integration.Utils;

public sealed class CleanWebAppFixture : WebApplicationFactory<IWebAppAssemblyLocator>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }

    public void Reset()
    {
        // Reset web app.
    }
}
