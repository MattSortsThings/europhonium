using Europhonium.Application.Admin.Placeholders;
using Europhonium.Contracts.Admin.Placeholders;
using Europhonium.Endpoints.Shared.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Endpoints.Admin.Placeholders;

internal sealed class GetGreetingsEndpoint :
    RailwayEndpoint<GetGreetingsRequest, GetGreetingsQuery, string[], Ok<GetGreetingsResponse>>
{
    public GetGreetingsEndpoint(ISender sender) : base(sender)
    {
    }

    public override void Configure()
    {
        Get("greetings");
        Group<PlaceholderEndpointGroup>();
        Description(builder => builder.Produces<GetGreetingsResponse>());
        Summary(summary => summary.Summary = "Generates the specified quantity of greetings");
    }

    private protected override GetGreetingsQuery MapToAppRequest(GetGreetingsRequest request) => new(request.Quantity);

    private protected override Ok<GetGreetingsResponse> MapToResult(string[] appResult) =>
        TypedResults.Ok(new GetGreetingsResponse(appResult));
}
