using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Modules.Public.Placeholders;

internal static class EndpointMapping
{
    internal static IEndpointRouteBuilder MapPlaceholdersEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGroup("placeholders")
            .MapGet("modulo/{dividend:int}/{modulus:int}", GetModulo.ExecuteAsync)
            .Produces<GetModulo.Response>()
            .WithName(nameof(GetModulo))
            .WithDisplayName("Get Modulo");

        return routeBuilder;
    }
}
