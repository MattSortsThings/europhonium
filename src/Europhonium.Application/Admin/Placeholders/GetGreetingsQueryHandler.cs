namespace Europhonium.Application.Admin.Placeholders;

internal sealed class GetGreetingsQueryHandler : IRequestHandler<GetGreetingsQuery, ErrorOr<string[]>>
{
    public async Task<ErrorOr<string[]>> Handle(GetGreetingsQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return Enumerable.Repeat("Hello World!", request.Quantity).ToArray();
    }
}
