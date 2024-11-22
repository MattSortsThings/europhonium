using MediatR;

namespace Europhonium.Application.Admin.Placeholders;

public sealed record GetGreetingsQuery(int Quantity, string Language) : IRequest<string[]>;
