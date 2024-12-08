using Europhonium.Shared.Infrastructure.DataAccess.EFCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Shared.Infrastructure;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Registers all the shared infrastructure services for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <param name="configuration">Contains service configuration properties for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    public static IServiceCollection AddSharedInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEFCoreServices(configuration);

        return services;
    }
}
