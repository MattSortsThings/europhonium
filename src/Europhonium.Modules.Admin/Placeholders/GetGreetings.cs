using ErrorOr;
using Europhonium.Shared.Infrastructure.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Europhonium.Modules.Admin.Placeholders;

public static class GetGreetings
{
    public static async Task<Results<Ok<Response>, ProblemHttpResult>> ExecuteAsync([AsParameters] Request request,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        Query query = new(request.Quantity, request.Language);

        ErrorOr<GreetingResource[]> result = await sender.Send(query, cancellationToken);

        return result.IsError ? result.FirstError.ToProblemHttpResult() : TypedResults.Ok(new Response(result.Value));
    }

    public sealed record Request
    {
        [FromQuery(Name = "quantity")]
        public int Quantity { get; init; }

        [FromQuery(Name = "language")]
        public Language Language { get; init; }
    }

    public sealed record Response(GreetingResource[] Greetings);

    private sealed record Query(int Quantity, Language Language) : IRequest<ErrorOr<GreetingResource[]>>;

    private sealed class Handler : IRequestHandler<Query, ErrorOr<GreetingResource[]>>
    {
        public async Task<ErrorOr<GreetingResource[]>> Handle(Query request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (request.Quantity < 1)
            {
                return Error.Validation("InvalidGreetingQuantity",
                    "Greetings quantity must be greater than 0.",
                    new Dictionary<string, object> { { "quantity", request.Quantity } });
            }

            var greeting = request.Language switch
            {
                Language.French => "Bonjour!",
                Language.Dutch => "Hoi!",
                _ => "Hi!"
            };

            return Enumerable.Repeat(new GreetingResource(greeting, request.Language), request.Quantity)
                .ToArray();
        }
    }
}
