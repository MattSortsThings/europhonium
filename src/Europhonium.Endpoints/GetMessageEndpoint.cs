using FastEndpoints;

namespace Europhonium.Endpoints;

public class GetMessageEndpoint : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Get("message");
        AllowAnonymous();
    }

    public override Task<string> ExecuteAsync(CancellationToken ct) => Task.FromResult("Hi there from Europhonium!");
}
