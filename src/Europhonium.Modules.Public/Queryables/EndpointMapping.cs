using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Modules.Public.Queryables;

internal static class EndpointMapping
{
    internal static IEndpointRouteBuilder MapQueryablesEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGroup("queryables")
            .WithTags("Queryables")
            .MapGet("countries", GetQueryableCountries.ExecuteAsync)
            .WithName(nameof(GetQueryableCountries))
            .WithDisplayName("Get Queryable Countries")
            .WithSummary("Retrieves all the queryable countries, ordered by country code")
            .Produces<GetQueryableCountries.Response>();

        return routeBuilder;
    }
}
