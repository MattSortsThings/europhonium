using Europhonium.Application.Public.Placeholders;
using Europhonium.Contracts.Public.Placeholders;
using Europhonium.Endpoints.Shared.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Endpoints.Public.Placeholders;

internal sealed class GetModuloEndpoint :
    RailwayEndpoint<GetModuloRequest, GetModuloQuery, GetModuloResult, Ok<GetModuloResponse>>
{
    public GetModuloEndpoint(ISender sender) : base(sender)
    {
    }

    public override void Configure()
    {
        Get("modulo/{dividend:int}/{modulus:int}");
        Group<PlaceholderEndpointGroup>();
        Description(builder => builder.Produces<GetModuloResponse>());
        Summary(summary => summary.Summary = "Performs a modulo calculation");
    }

    private protected override GetModuloQuery MapToAppRequest(GetModuloRequest request) =>
        new(request.Dividend, request.Modulus);

    private protected override Ok<GetModuloResponse> MapToResult(GetModuloResult appResult) =>
        TypedResults.Ok(new GetModuloResponse(appResult.Dividend,
            appResult.Modulus,
            appResult.Remainder,
            DateOnly.FromDateTime(DateTime.Now)));
}
