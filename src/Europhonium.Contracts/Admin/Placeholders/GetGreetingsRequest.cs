using FastEndpoints;

namespace Europhonium.Contracts.Admin.Placeholders;

public sealed record GetGreetingsRequest
{
    [QueryParam]
    [BindFrom("quantity")]
    public int Quantity { get; init; }

    [QueryParam]
    [BindFrom("language")]
    public GreetingsLanguage Language { get; init; }
}
