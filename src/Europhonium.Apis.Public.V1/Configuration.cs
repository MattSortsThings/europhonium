using Europhonium.Apis.Public.V1.Placeholders.ModuloCalculation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Apis.Public.V1;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class Configuration
{
    /// <summary>
    ///     Maps all the endpoints for Version 1 of the Public API.
    /// </summary>
    /// <param name="routeBuilder">The root-level endpoint route builder for the web application.</param>
    /// <returns>The same <see cref="IEndpointRouteBuilder" /> instance, so that method invocations can be chained.</returns>
    public static IEndpointRouteBuilder MapPublicApiV1Endpoints(this IEndpointRouteBuilder routeBuilder)
    {
        RouteGroupBuilder group = routeBuilder.MapGroup("public/api/v1.0");

        group.MapGetModuloCalculation();

        return routeBuilder;
    }
}
