namespace Europhonium.Contracts.Public.Placeholders;

public sealed record GetModuloResponse
{
    public int Dividend { get; init; }

    public int Modulus { get; init; }

    public int Remainder { get; init; }
}
