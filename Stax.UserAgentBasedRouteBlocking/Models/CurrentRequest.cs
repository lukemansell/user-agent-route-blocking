using Microsoft.AspNetCore.Http;

namespace Stax.UserAgentBasedRouteBlocking.Models;

internal class CurrentRequest
{
    public CurrentRequest()
    {
    }

    public CurrentRequest(string? referrer, PathString path, string? userAgent)
    {
        Referrer = referrer;
        Path = path;
        UserAgent = userAgent;
    }

    internal PathString Path { get; set; }

    internal string? Referrer { get; set; }
    
    internal string? UserAgent { get; set; }
}