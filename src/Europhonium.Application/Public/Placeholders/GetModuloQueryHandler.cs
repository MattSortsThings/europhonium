namespace Europhonium.Application.Public.Placeholders;

internal sealed class GetModuloQueryHandler : IRequestHandler<GetModuloQuery, ErrorOr<GetModuloResult>>
{
    public async Task<ErrorOr<GetModuloResult>> Handle(GetModuloQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (request.Modulus == 0)
        {
            return Error.Validation("DivideByZero", "Modulus cannot be zero.");
        }

        return new GetModuloResult(request.Dividend, request.Modulus, request.Dividend % request.Modulus);
    }
}
