using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Modules.Admin.Placeholders;

internal static class EndpointMapping
{
    internal static IEndpointRouteBuilder MapPlaceholdersEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGroup("placeholders")
            .MapGet("greetings", GetGreetings.ExecuteAsync)
            .Produces<GetGreetings.Response>()
            .WithName(nameof(GetGreetings))
            .WithDisplayName("Get Greetings");

        return routeBuilder;
    }
}
