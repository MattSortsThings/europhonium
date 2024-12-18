using Europhonium.Modules.AdminApi.V1.Placeholders.Greetings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Modules.AdminApi.V1;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class EndpointMapping
{
    /// <summary>
    ///     Maps all the endpoints for the Admin API V1 Module.
    /// </summary>
    /// <param name="routeBuilder">The base endpoint route builder for the web application.</param>
    /// <returns>The same <see cref="IEndpointRouteBuilder" /> instance, so that method invocations can be chained.</returns>
    public static IEndpointRouteBuilder MapAdminApiV1Endpoints(this IEndpointRouteBuilder routeBuilder)
    {
        RouteGroupBuilder minorVersion0Group = routeBuilder.MapGroup("admin/api/v1.0")
            .WithGroupName(GroupNames.AdminApi.V1.Point0);

        minorVersion0Group.MapGetGreetings();

        return routeBuilder;
    }
}
