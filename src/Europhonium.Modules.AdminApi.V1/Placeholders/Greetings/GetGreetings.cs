using ErrorOr;
using Europhonium.Shared.Domain.Placeholders.Greetings;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Modules.AdminApi.V1.Placeholders.Greetings;

public static class GetGreetings
{
    internal static readonly string EndpointName = typeof(GetGreetings).FullName!;

    internal static IEndpointRouteBuilder MapGetGreetings(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("placeholders/greetings", ExecuteAsync)
            .WithName(EndpointName)
            .WithDisplayName("Get Greetings")
            .Produces<Response>()
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags(EndpointTags.Placeholders);

        return routeBuilder;
    }

    public static async Task<Results<Ok<Response>, BadRequest>> ExecuteAsync(
        [AsParameters] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken = default)
    {
        ErrorOr<GreetingDto[]> result = await sender.Send(request.ToQuery(), cancellationToken);

        return result.IsError ? TypedResults.BadRequest() : result.Value.ToOkResult();
    }

    private static Query ToQuery(this Request request) => new(request.Quantity, request.Language.ToLanguage());

    private static Ok<Response> ToOkResult(this GreetingDto[] greetings) => TypedResults.Ok(new Response(greetings));

    public sealed record Request
    {
        [FromQuery(Name = "quantity")]
        public int Quantity { get; init; }

        [FromQuery(Name = "language")]
        public LanguageOption Language { get; init; }
    }

    public sealed record Response(GreetingDto[] Greetings);

    private sealed record Query(int Quantity, Language Language) : IRequest<ErrorOr<GreetingDto[]>>;

    private sealed class Handler : IRequestHandler<Query, ErrorOr<GreetingDto[]>>
    {
        public async Task<ErrorOr<GreetingDto[]>> Handle(Query query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask.ConfigureAwait(false);

            var message = query.Language switch
            {
                Language.French => "Bonjour!",
                Language.Dutch => "Hoi!",
                Language.English => "Hi!",
                _ => "Hi!"
            };

            return Enumerable.Range(0, query.Quantity)
                .Select(_ => new GreetingDto(message, query.Language.ToLanguageOption(), Guid.NewGuid()))
                .ToArray();
        }
    }
}
