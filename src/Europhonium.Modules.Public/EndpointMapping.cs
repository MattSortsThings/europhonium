using Europhonium.Modules.Public.Queryables;
using Europhonium.Shared.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Modules.Public;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class EndpointMapping
{
    /// <summary>
    ///     Maps all the Public Module endpoints for the web application.
    /// </summary>
    /// <param name="routeBuilder">The root-level endpoint route builder for the web application.</param>
    /// <returns>The same <see cref="IEndpointRouteBuilder" /> instance, so that method invocations can be chained.</returns>
    public static IEndpointRouteBuilder MapPublicEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGroup("public")
            .RequireAuthorization(SecurityConstants.Policies.AdminOrUser)
            .WithTags("public")
            .MapQueryablesEndpoints();

        return routeBuilder;
    }
}
