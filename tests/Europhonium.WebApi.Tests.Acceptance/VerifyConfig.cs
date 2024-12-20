using System.Runtime.CompilerServices;

namespace Europhonium.WebApi.Tests.Acceptance;

public static class VerifyConfig
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifierSettings.DontIgnoreEmptyCollections();
        VerifierSettings.ScrubMember("traceId");
        VerifierSettings.ScrubInlineGuids();
        VerifierSettings.ScrubMember("dateRequested");
    }
}
