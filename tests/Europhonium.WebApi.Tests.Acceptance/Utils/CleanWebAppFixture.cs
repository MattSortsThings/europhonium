using Europhonium.Shared.Infrastructure.DataAccess;
using Europhonium.Shared.Infrastructure.DataAccess.EFCore;
using Europhonium.WebApi.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public sealed partial class CleanWebAppFixture : WebApplicationFactory<IWebApiAssemblyLocator>,
    IAsyncLifetime
{
    private static readonly Uri BaseUri = new("http://localhost:5083");

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

    internal void Reset()
    {
        using IServiceScope scope = Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<WebAppDbContext>();
        dbContext.Countries.ExecuteDelete();
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

        builder.UseSetting(DataAccessConstants.DbConnectionStringConfigPath, _dbContainer.GetConnectionString());
    }
}
