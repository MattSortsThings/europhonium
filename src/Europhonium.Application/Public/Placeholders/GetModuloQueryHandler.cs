using MediatR;

namespace Europhonium.Application.Public.Placeholders;

internal sealed class GetModuloQueryHandler : IRequestHandler<GetModuloQuery, int>
{
    public Task<int> Handle(GetModuloQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(request.Dividend % request.Modulus);
}
