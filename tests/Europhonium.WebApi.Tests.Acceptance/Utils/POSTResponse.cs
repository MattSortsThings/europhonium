using System.Net;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public readonly record struct POSTResponse(HttpStatusCode StatusCode, string Content, string? Location);
