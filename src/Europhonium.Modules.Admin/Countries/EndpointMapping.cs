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

        group.MapGet("/{countryId:guid}", GetCountry.ExecuteAsync)
            .WithName(nameof(GetCountry))
            .WithDisplayName("Get Country")
            .WithSummary("Retrieves a single country")
            .Produces<GetCountry.Response>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return routeBuilder;
    }
}
