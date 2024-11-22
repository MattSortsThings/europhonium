using Europhonium.Application.Admin.Placeholders;
using Europhonium.Contracts.Admin.Placeholders;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Endpoints.Admin.Placeholders.Endpoints;

internal sealed class GetGreetingsEndpoint(ISender sender) : Endpoint<GetGreetingsRequest, Ok<GetGreetingsResponse>>
{
    public override void Configure()
    {
        Get("greetings");
        Group<PlaceholdersEndpointSubGroup>();
    }

    public override async Task<Ok<GetGreetingsResponse>> ExecuteAsync(GetGreetingsRequest req, CancellationToken ct)
    {
        GetGreetingsQuery query = new(req.Quantity, req.Language.ToString());

        string[]? result = await sender.Send(query, ct);

        return TypedResults.Ok<GetGreetingsResponse>(new GetGreetingsResponse(result));
    }
}
