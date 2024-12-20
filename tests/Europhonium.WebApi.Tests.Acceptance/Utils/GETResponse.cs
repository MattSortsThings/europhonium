using System.Net;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public sealed record GETResponse(HttpStatusCode StatusCode, string Content);
