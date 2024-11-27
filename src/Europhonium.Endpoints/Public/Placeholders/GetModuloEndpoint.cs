using Europhonium.Application.Public.Placeholders;
using Europhonium.Contracts.Public.Placeholders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Endpoints.Public.Placeholders;

internal sealed class GetModuloEndpoint(ISender sender) : Endpoint<GetModuloRequest, Ok<GetModuloResponse>>
{
    public override void Configure()
    {
        Get("modulo/{dividend:int}/{modulus:int}");
        Group<PlaceholderEndpointGroup>();
        Description(builder => builder.Produces<GetModuloResponse>());
        Summary(summary => summary.Summary = "Performs a modulo calculation");
    }

    public override async Task<Ok<GetModuloResponse>> ExecuteAsync(GetModuloRequest req, CancellationToken ct)
    {
        GetModuloQuery query = new(req.Dividend, req.Modulus);

        GetModuloResult? result = await sender.Send(query, ct);

        return TypedResults.Ok(new GetModuloResponse(result.Dividend,
            result.Modulus,
            result.Remainder,
            DateOnly.FromDateTime(DateTime.UtcNow)));
    }
}
