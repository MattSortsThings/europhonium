using Europhonium.Application.Admin.Placeholders;
using Europhonium.Contracts.Admin.Placeholders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Endpoints.Admin.Placeholders;

internal sealed class GetGreetingsEndpoint(ISender sender) : Endpoint<GetGreetingsRequest, Ok<GetGreetingsResponse>>
{
    public override void Configure()
    {
        Get("greetings");
        Group<PlaceholderEndpointGroup>();
        Description(builder => builder.Produces<GetGreetingsResponse>());
        Summary(summary => summary.Summary = "Generates the specified quantity of greetings");
    }

    public override async Task<Ok<GetGreetingsResponse>> ExecuteAsync(GetGreetingsRequest req, CancellationToken ct)
    {
        GetGreetingsQuery query = new(req.Quantity);

        var greetings = await sender.Send(query, ct);

        return TypedResults.Ok(new GetGreetingsResponse(greetings));
    }
}
