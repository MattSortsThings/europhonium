namespace Europhonium.Contracts.Public.Placeholders;

public sealed record GetModuloRequest
{
    [BindFrom("dividend")]
    public int Dividend { get; init; }

    [BindFrom("modulus")]
    public int Modulus { get; init; }
}
