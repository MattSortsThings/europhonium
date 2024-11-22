using MediatR;

namespace Europhonium.Application.Admin.Placeholders;

internal sealed class GetGreetingsQueryHandler : IRequestHandler<GetGreetingsQuery, string[]>
{
    public Task<string[]> Handle(GetGreetingsQuery request, CancellationToken cancellationToken)
    {
        string message = request.Language switch
        {
            "French" => "Bonjour!",
            "Dutch" => "Hoi!",
            _ => "Hi!"
        };

        return Task.FromResult(Enumerable.Repeat(message, request.Quantity).ToArray());
    }
}
