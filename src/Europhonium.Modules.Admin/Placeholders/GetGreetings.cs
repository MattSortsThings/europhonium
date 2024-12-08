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
        Query query = new(request.Quantity, request.Language);
        GreetingResource[]? greetings = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(new Response(greetings));
    }

    public sealed record Request
    {
        [FromQuery(Name = "quantity")]
        public int Quantity { get; init; }

        [FromQuery(Name = "language")]
        public Language Language { get; init; }
    }

    public sealed record Response(GreetingResource[] Greetings);

    private sealed record Query(int Quantity, Language Language) : IRequest<GreetingResource[]>;

    private sealed class Handler : IRequestHandler<Query, GreetingResource[]>
    {
        public Task<GreetingResource[]> Handle(Query request, CancellationToken cancellationToken)
        {
            var greeting = request.Language switch
            {
                Language.French => "Bonjour!",
                Language.Dutch => "Hoi!",
                _ => "Hi!"
            };

            GreetingResource[] resources = Enumerable.Repeat(new GreetingResource(greeting, request.Language), request.Quantity)
                .ToArray();

            return Task.FromResult(resources);
        }
    }
}
