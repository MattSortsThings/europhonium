namespace Europhonium.Modules.Public.Placeholders;

public sealed record ModuloResource
{
    public int Dividend { get; init; }

    public int Modulus { get; init; }

    public int Remainder { get; init; }
}
