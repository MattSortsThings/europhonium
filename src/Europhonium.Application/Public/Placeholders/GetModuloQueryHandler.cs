namespace Europhonium.Application.Public.Placeholders;

internal sealed class GetModuloQueryHandler : IRequestHandler<GetModuloQuery, GetModuloResult>
{
    public async Task<GetModuloResult> Handle(GetModuloQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var remainder = request.Dividend % request.Modulus;

        return new GetModuloResult(request.Dividend, request.Modulus, remainder);
    }
}
