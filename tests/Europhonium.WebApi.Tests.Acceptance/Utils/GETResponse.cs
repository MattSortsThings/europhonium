using System.Net;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public readonly record struct GETResponse(HttpStatusCode StatusCode, string Content);
