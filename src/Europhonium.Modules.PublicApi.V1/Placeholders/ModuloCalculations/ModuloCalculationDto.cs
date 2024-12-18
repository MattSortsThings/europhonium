namespace Europhonium.Modules.PublicApi.V1.Placeholders.ModuloCalculations;

public sealed record ModuloCalculationDto(int Dividend, int Modulus, int Remainder, DateOnly DateRequested);
