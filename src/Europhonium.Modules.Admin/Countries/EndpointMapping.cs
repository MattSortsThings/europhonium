using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Modules.Admin.Countries;

internal static class EndpointMapping
{
    internal static IEndpointRouteBuilder MapCountriesEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        RouteGroupBuilder group = routeBuilder.MapGroup("countries")
            .WithTags("Countries");

        group.MapPost("/", CreateCountry.ExecuteAsync)
            .WithName(nameof(CreateCountry))
            .WithDisplayName("Create Country")
            .WithSummary("Creates a new country")
            .Produces<CreateCountry.Response>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict);

        return routeBuilder;
    }
}
