using MediatR;

namespace Europhonium.Application.Public.Placeholders;

public sealed record GetModuloQuery(int Dividend, int Modulus) : IRequest<int>;
