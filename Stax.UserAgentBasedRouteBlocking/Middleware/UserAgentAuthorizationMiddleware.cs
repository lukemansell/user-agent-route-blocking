using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stax.UserAgentBasedRouteBlocking.Helpers;

namespace Stax.UserAgentBasedRouteBlocking.Middleware;

public class UserAgentAuthorizationMiddleware
{
    private readonly RequestDelegate _requestDelegate;
    private readonly IConfiguration _configuration;

    public UserAgentAuthorizationMiddleware(RequestDelegate requestDelegate, IConfiguration configuration)
    {
        _requestDelegate = requestDelegate;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var requestInfo = RequestHelper.GetCurrentRequestInfo(httpContext.Request);
        var userConfiguration = UserConfigurationHelper.GetUserConfiguration(_configuration);
        
        if (userConfiguration.PathsToAuthorize.Any(path=> requestInfo.Path.StartsWithSegments(path)))
        {
            if (RequestHelper.ShouldAllowRequest(requestInfo, userConfiguration))
            {
                await _requestDelegate.Invoke(httpContext).ConfigureAwait(false);
                return;
            }
            
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            await _requestDelegate.Invoke(httpContext).ConfigureAwait(false);
        }
    }
}