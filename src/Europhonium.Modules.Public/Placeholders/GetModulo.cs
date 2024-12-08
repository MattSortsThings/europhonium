using ErrorOr;
using Europhonium.Shared.Infrastructure.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Europhonium.Modules.Public.Placeholders;

public static class GetModulo
{
    public static async Task<Results<Ok<Response>, ProblemHttpResult>> ExecuteAsync([FromRoute(Name = "dividend")] int dividend,
        [FromRoute(Name = "modulus")] int modulus,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        ErrorOr<ModuloResource> result = await sender.Send(new Query(dividend, modulus), cancellationToken);

        return result.IsError ? result.FirstError.ToProblemHttpResult() : TypedResults.Ok(new Response(result.Value));
    }

    public sealed record Response(ModuloResource Modulo);

    private sealed record Query(int Dividend, int Modulus) : IRequest<ErrorOr<ModuloResource>>;

    private sealed class Handler : IRequestHandler<Query, ErrorOr<ModuloResource>>
    {
        public async Task<ErrorOr<ModuloResource>> Handle(Query request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (request.Modulus == 0)
            {
                return Error.Validation("DivideByZero", "Modulus cannot be 0.");
            }

            var remainder = request.Dividend % request.Modulus;

            return new ModuloResource { Dividend = request.Dividend, Modulus = request.Modulus, Remainder = remainder };
        }
    }
}
