using Europhonium.Modules.Admin.Countries;
using Europhonium.Shared.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Modules.Admin;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class EndpointMapping
{
    /// <summary>
    ///     Maps all the Admin Module endpoints for the web application.
    /// </summary>
    /// <param name="routeBuilder">The root-level endpoint route builder for the web application.</param>
    /// <returns>The same <see cref="IEndpointRouteBuilder" /> instance, so that method invocations can be chained.</returns>
    public static IEndpointRouteBuilder MapAdminEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGroup("admin")
            .RequireAuthorization(SecurityConstants.Policies.AdminOnly)
            .WithTags("Admin")
            .MapCountriesEndpoints();

        return routeBuilder;
    }
}
