namespace Europhonium.Contracts.Public.Placeholders;

public sealed record GetModuloResponse(int Dividend, int Modulus, int Remainder, DateOnly DateRequested);
