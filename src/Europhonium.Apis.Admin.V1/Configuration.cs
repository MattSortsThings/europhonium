using Europhonium.Apis.Admin.V1.Placeholders.Greetings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Apis.Admin.V1;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class Configuration
{
    /// <summary>
    ///     Maps all the endpoints for Version 1 of the Admin API.
    /// </summary>
    /// <param name="routeBuilder">The root-level endpoint route builder for the web application.</param>
    /// <returns>The same <see cref="IEndpointRouteBuilder" /> instance, so that method invocations can be chained.</returns>
    public static IEndpointRouteBuilder MapAdminApiV1Endpoints(this IEndpointRouteBuilder routeBuilder)
    {
        RouteGroupBuilder group = routeBuilder.MapGroup("admin/api/v1.0");

        group.MapGetGreetings();

        return routeBuilder;
    }
}
