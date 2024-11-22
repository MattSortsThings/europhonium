using Europhonium.Application.Public.Placeholders;
using Europhonium.Contracts.Public.Placeholders;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Endpoints.Public.Placeholders.Endpoints;

internal sealed class GetModuloEndpoint(ISender sender) : Endpoint<GetModuloRequest, Ok<GetModuloResponse>>
{
    public override void Configure()
    {
        Get("modulo/{dividend:int}/{modulus:int}");
        Group<PlaceholdersEndpointSubGroup>();
    }

    public override async Task<Ok<GetModuloResponse>> ExecuteAsync(GetModuloRequest req, CancellationToken ct)
    {
        GetModuloQuery query = new(req.Dividend, req.Modulus);

        int result = await sender.Send(query, ct);

        return TypedResults.Ok(new GetModuloResponse { Dividend = req.Dividend, Modulus = req.Modulus, Remainder = result });
    }
}
