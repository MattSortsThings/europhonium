using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Europhonium.Modules.Public.Placeholders;

public static class GetModulo
{
    public static async Task<Ok<Response>> ExecuteAsync([FromRoute(Name = "dividend")] int dividend,
        [FromRoute(Name = "modulus")] int modulus,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var remainder = await sender.Send(new Query(dividend, modulus), cancellationToken);

        return TypedResults.Ok(new Response(dividend, modulus, remainder, Guid.NewGuid()));
    }

    public sealed record Response(int Dividend, int Modulus, int Remainder, Guid RequestId);

    private sealed record Query(int Dividend, int Modulus) : IRequest<int>;

    private sealed class Handler : IRequestHandler<Query, int>
    {
        public Task<int> Handle(Query request, CancellationToken cancellationToken) =>
            Task.FromResult(request.Dividend % request.Modulus);
    }
}
