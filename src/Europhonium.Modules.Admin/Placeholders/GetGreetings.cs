using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Europhonium.Modules.Admin.Placeholders;

public static class GetGreetings
{
    public static async Task<Ok<Response>> ExecuteAsync([AsParameters] Request request,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        Query query = new(request.Quantity, request.Language.ToString());
        string[] greetings = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(new Response(greetings, request.Language));
    }

    public sealed record Request
    {
        [FromQuery(Name = "quantity")]
        public int Quantity { get; init; }

        [FromQuery(Name = "language")]
        public Language Language { get; init; }
    }

    public sealed record Response(string[] Greetings, Language Language);

    private sealed record Query(int Quantity, string Language) : IRequest<string[]>;

    private sealed class Handler : IRequestHandler<Query, string[]>
    {
        public Task<string[]> Handle(Query request, CancellationToken cancellationToken)
        {
            var greeting = request.Language switch
            {
                "French" => "Bonjour!",
                "Dutch" => "Hoi!",
                _ => "Hi!"
            };

            return Task.FromResult(Enumerable.Repeat(greeting, request.Quantity).ToArray());
        }
    }
}
