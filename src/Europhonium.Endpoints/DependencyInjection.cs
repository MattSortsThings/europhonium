using Europhonium.Endpoints.Shared.Documentation;
using Europhonium.Endpoints.Shared.ErrorHandling;
using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Endpoints;

public static class DependencyInjection
{
    /// <summary>
    ///     Registers all the endpoints-layer services for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    public static IServiceCollection AddEndpointsServices(this IServiceCollection services)
    {
        services.AddFastEndpoints()
            .AddDocumentationServices()
            .AddErrorHandlingServices();

        return services;
    }
}
