using System.Net;

namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public readonly record struct GetResponse(HttpStatusCode StatusCode, string Content);
