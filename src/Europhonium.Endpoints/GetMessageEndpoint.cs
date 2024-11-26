using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Endpoints;

public class GetMessageEndpoint : EndpointWithoutRequest<Ok<string>>
{
    public override void Configure()
    {
        Get("message");
        AllowAnonymous();
    }

    public override Task<Ok<string>> ExecuteAsync(CancellationToken ct) =>
        Task.FromResult(TypedResults.Ok("Hi there from Europhonium!"));
}
