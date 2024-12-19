using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Apis.Public.V1.Placeholders.ModuloCalculation;

public static class GetModuloCalculation
{
    internal static IEndpointRouteBuilder MapGetModuloCalculation(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("placeholders/modulo-calculations/{dividend:int}/{modulus:int}", ExecuteAsync)
            .WithName(nameof(GetModuloCalculation))
            .WithDisplayName("Get Modulo Calculation")
            .Produces<Response>();

        return routeBuilder;
    }

    public static async Task<Results<Ok<Response>, BadRequest>> ExecuteAsync(
        [FromRoute(Name = "dividend")] int dividend,
        [FromRoute(Name = "modulus")] int modulus,
        [FromServices] ISender sender,
        CancellationToken cancellationToken = default)
    {
        Query query = new(dividend, modulus);

        ErrorOr<ModuloCalculationDto> result = await sender.Send(query, cancellationToken);

        return result.IsError ? TypedResults.BadRequest() : TypedResults.Ok(new Response(result.Value));
    }

    public sealed record Response(ModuloCalculationDto ModuloCalculation);

    private sealed record Query(int Dividend, int Modulus) : IRequest<ErrorOr<ModuloCalculationDto>>;

    private sealed class Handler : IRequestHandler<Query, ErrorOr<ModuloCalculationDto>>
    {
        public async Task<ErrorOr<ModuloCalculationDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask.ConfigureAwait(false);

            var (dividend, modulus) = query;
            var remainder = dividend % modulus;

            DateOnly dateRequested = DateOnly.FromDateTime(DateTime.UtcNow);

            return new ModuloCalculationDto(dividend, modulus, remainder, dateRequested);
        }
    }
}
