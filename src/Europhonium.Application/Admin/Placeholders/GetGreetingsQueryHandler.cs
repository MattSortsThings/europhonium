namespace Europhonium.Application.Admin.Placeholders;

internal sealed class GetGreetingsQueryHandler : IRequestHandler<GetGreetingsQuery, string[]>
{
    public Task<string[]> Handle(GetGreetingsQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(Enumerable.Repeat("Hello World", request.Quantity).ToArray());
}
