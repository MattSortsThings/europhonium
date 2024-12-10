using Europhonium.Shared.Infrastructure.DataAccess;
using Europhonium.Shared.Infrastructure.DataAccess.EFCore;
using Europhonium.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Testcontainers.MsSql;

namespace Europhonium.Modules.Public.Tests.Integration.Utils;

public sealed class SeededWebAppFixture : WebApplicationFactory<IWebApiAssemblyLocator>, IAsyncLifetime
{
    private const string SeedingScriptSubPath = "Scripts/seed_db_with_integration_test_data.sql";
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using IServiceScope scope = Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<WebAppDbContext>();
        await dbContext.Database.MigrateAsync();

        await SeedDbWithIntegrationTestDataAsync(dbContext);
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

    private static async Task SeedDbWithIntegrationTestDataAsync(WebAppDbContext dbContext)
    {
        var embeddedProvider = new EmbeddedFileProvider(typeof(SeededWebAppFixture).Assembly);
        await using Stream reader = embeddedProvider.GetFileInfo(SeedingScriptSubPath).CreateReadStream();
        using var streamReader = new StreamReader(reader);

        var sql = await streamReader.ReadToEndAsync();

        await dbContext.Database.ExecuteSqlRawAsync(sql);
    }
}
