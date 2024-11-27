using Europhonium.Endpoints.Shared.Documentation;
using Europhonium.Endpoints.Shared.ErrorHandling;
using Europhonium.Endpoints.Shared.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Endpoints;

public static class DependencyInjection
{
    /// <summary>
    ///     Registers all the endpoints-layer services for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <param name="configuration">Contains configuration properties for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    public static IServiceCollection AddEndpointsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFastEndpoints()
            .AddDocumentationServices()
            .AddErrorHandlingServices()
            .AddSecurityServices(configuration);

        return services;
    }
}
