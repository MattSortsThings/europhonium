using ErrorOr;
using Europhonium.Domain.Placeholders.Greetings;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Europhonium.Apis.Admin.V1.Placeholders.Greetings;

public static class GetGreetings
{
    internal static IEndpointRouteBuilder MapGetGreetings(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("placeholders/greetings", ExecuteAsync)
            .WithName(nameof(GetGreetings))
            .WithDisplayName("Get Greetings")
            .Produces<Response>();

        return routeBuilder;
    }

    public static async Task<Results<Ok<Response>, BadRequest>> ExecuteAsync(
        [AsParameters] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken = default)
    {
        Query query = new(request.Quantity, request.Language.ToDomainLanguage());

        ErrorOr<GreetingDto[]> result = await sender.Send(query, cancellationToken);

        return result.IsError ? TypedResults.BadRequest() : TypedResults.Ok(new Response(result.Value));
    }

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

            (var quantity, Language language) = query;

            var message = language switch
            {
                Language.French => "Bonjour!",
                Language.Dutch => "Hoi!",
                Language.English => "Hi!",
                _ => "Hi!"
            };

            return Enumerable.Range(0, quantity).Select(_ =>
                new GreetingDto(message, language.ToApiLanguage(), Guid.NewGuid())
            ).ToArray();
        }
    }
}
