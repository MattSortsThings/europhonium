using ErrorOr;

namespace Europhonium.Shared.Domain.Tests.Unit.Utils;

internal static class ErrorExtensions
{
    internal static ErrorAssertions Should(this Error error) => new(error);
}
