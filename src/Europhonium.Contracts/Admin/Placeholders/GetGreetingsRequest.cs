namespace Europhonium.Contracts.Admin.Placeholders;

public sealed record GetGreetingsRequest
{
    [QueryParam]
    [BindFrom("quantity")]
    public int Quantity { get; init; }
}
