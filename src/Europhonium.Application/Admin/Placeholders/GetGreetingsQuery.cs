namespace Europhonium.Application.Admin.Placeholders;

public sealed record GetGreetingsQuery(int Quantity) : IRequest<ErrorOr<string[]>>;
