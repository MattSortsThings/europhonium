namespace Europhonium.WebApi.Tests.Acceptance.Utils;

public interface IWebApiDriver
{
    public void UseApi(string apiName, int majorVersion = 1, int minorVersion = 0);

    public Task<GETResponse> GETAsync(string route);
}
