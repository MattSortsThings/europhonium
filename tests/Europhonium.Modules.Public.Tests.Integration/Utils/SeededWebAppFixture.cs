using Europhonium.Shared.Infrastructure.DataAccess;
using Europhonium.Shared.Infrastructure.DataAccess.EFCore;
using Europhonium.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Europhonium.Modules.Public.Tests.Integration.Utils;

public sealed class SeededWebAppFixture : WebApplicationFactory<IWebApiAssemblyLocator>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using IServiceScope scope = Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<WebAppDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting(DataAccessConstants.DbConnectionStringConfigPath, _dbContainer.GetConnectionString());
    }
}
