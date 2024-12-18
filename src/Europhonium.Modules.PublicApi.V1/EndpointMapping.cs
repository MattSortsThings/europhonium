using Europhonium.Modules.PublicApi.V1.Placeholders.ModuloCalculations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Modules.PublicApi.V1;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class EndpointMapping
{
    /// <summary>
    ///     Maps all the endpoints for the Public API V1 Module.
    /// </summary>
    /// <param name="routeBuilder">The base endpoint route builder for the web application.</param>
    /// <returns>The same <see cref="IEndpointRouteBuilder" /> instance, so that method invocations can be chained.</returns>
    public static IEndpointRouteBuilder MapPublicApiV1Endpoints(this IEndpointRouteBuilder routeBuilder)
    {
        RouteGroupBuilder minorVersion0Group = routeBuilder.MapGroup("public/api/v1.0")
            .WithGroupName(GroupNames.PublicApi.V1.Point0);

        minorVersion0Group.MapGetModuloCalculation();

        return routeBuilder;
    }
}
