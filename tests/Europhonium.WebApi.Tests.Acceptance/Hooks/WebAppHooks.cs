using Europhonium.WebApi.Tests.Acceptance.Utils;
using Reqnroll;
using Reqnroll.BoDi;

namespace Europhonium.WebApi.Tests.Acceptance.Hooks;

[Binding]
public static class WebAppHooks
{
    [BeforeTestRun]
    public static async Task InitializeCleanWebAppFixtureAsync(IObjectContainer container)
    {
        CleanWebAppFixture cleanWebAppFixture = new();
        await cleanWebAppFixture.InitializeAsync();

        container.RegisterInstanceAs(cleanWebAppFixture);
        container.RegisterInstanceAs(cleanWebAppFixture, typeof(IHttpClientProvider));
    }

    [AfterScenario]
    public static void ResetCleanWebAppFixture(IObjectContainer container)
    {
        var cleanWebAppFixture = container.Resolve<CleanWebAppFixture>();

        cleanWebAppFixture.Reset();
    }

    [AfterTestRun]
    public static async Task DisposeOfCleanWebAppFixtureAsync(IObjectContainer container)
    {
        var cleanWebAppFixture = container.Resolve<CleanWebAppFixture>();

        await cleanWebAppFixture.DisposeAsync();
    }
}
