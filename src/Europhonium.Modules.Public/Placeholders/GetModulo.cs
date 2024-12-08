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
        ModuloResource? modulo = await sender.Send(new Query(dividend, modulus), cancellationToken);

        return TypedResults.Ok(new Response(modulo));
    }

    public sealed record Response(ModuloResource Modulo);

    private sealed record Query(int Dividend, int Modulus) : IRequest<ModuloResource>;

    private sealed class Handler : IRequestHandler<Query, ModuloResource>
    {
        public Task<ModuloResource> Handle(Query request, CancellationToken cancellationToken)
        {
            var remainder = request.Dividend % request.Modulus;

            return Task.FromResult(new ModuloResource
            {
                Dividend = request.Dividend,
                Modulus = request.Modulus,
                Remainder = remainder
            });
        }
    }
}
