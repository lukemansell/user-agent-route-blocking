using Microsoft.AspNetCore.Builder;
using Stax.UserAgentBasedRouteBlocking.Middleware;

namespace Stax.UserAgentBasedRouteBlocking.Extensions;

public static class UserAgentAuthorizationExtension
{
    /// <summary>
    /// Enables middleware which "authorizes" access to certain routes based on configuration set in appsettings.json
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseUserAgentBasedRouteBlocking(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<UserAgentAuthorizationMiddleware>();
    }
}