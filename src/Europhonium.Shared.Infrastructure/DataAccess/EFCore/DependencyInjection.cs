using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Shared.Infrastructure.DataAccess.EFCore;

public static class DependencyInjection
{
    /// <summary>
    ///     Registers the EF Core database context services for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <param name="configuration">Contains service configuration properties for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    public static IServiceCollection AddEFCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WebAppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(DataAccessConstants.DbConnectionStringConfigKey))
                .UseSnakeCaseNamingConvention()
                .UseEnumCheckConstraints()
                .UseExceptionProcessor());

        return services;
    }
}
