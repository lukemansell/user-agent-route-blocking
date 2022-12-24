using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Stax.UserAgentBasedRouteBlocking.Models;

namespace Stax.UserAgentBasedRouteBlocking.Helpers;

internal static class RequestHelper
{
    /// <summary>
    /// Gets the current HTTP Request and returns it as a <see cref="CurrentRequest">
    /// </summary>
    /// <param name="request"></param>
    /// <returns>CurrentRequest</returns>
    internal static CurrentRequest GetCurrentRequestInfo(HttpRequest request) =>
        new(referrer: request.Headers[HeaderNames.Referer].ToString(), 
            path: request.Path,
            userAgent: request.Headers[HeaderNames.UserAgent].ToString()
            );

    /// <summary>
    /// Decides whether or not the current request given the users configuration should be allowed. Checks against
    /// possibilities of the user using overrides (allow all)
    /// </summary>
    /// <param name="currentRequest"></param>
    /// <param name="userConfiguration"></param>
    /// <returns></returns>
    internal static bool ShouldAllowRequest(CurrentRequest currentRequest, UserConfiguration userConfiguration)
    {
        return AllowAllReferrersUserAgentsEnabled(currentRequest, userConfiguration) ||
               AllowAllReferrersUserAgentList(currentRequest, userConfiguration) ||
               AllowReferrersListAllUserAgents(currentRequest, userConfiguration) || 
               AllowListReferrersListAllUserAgents(currentRequest, userConfiguration);
    }

    /// <summary>
    /// If the user has enabled all referrers, all user agents then the current path does not matter and we don't need
    /// to check anything as it
    /// </summary>
    /// <param name="currentRequest"></param>
    /// <param name="userConfiguration"></param>
    /// <returns></returns>
    private static bool AllowAllReferrersUserAgentsEnabled(CurrentRequest currentRequest, UserConfiguration userConfiguration)
    {
        return userConfiguration.ReferrerOptions.AllowAllReferrers 
               && userConfiguration.UserAgentOptions.AllowAllUserAgents;
    }

    /// <summary>
    /// If the user has allowed all referrers but has the option to only allow certain user agents, and the path matches
    /// </summary>
    /// <param name="currentRequest"></param>
    /// <param name="userConfiguration"></param>
    /// <returns></returns>
    private static bool AllowAllReferrersUserAgentList(CurrentRequest currentRequest,
        UserConfiguration userConfiguration)
    {
        return userConfiguration.ReferrerOptions.AllowAllReferrers
               && userConfiguration.UserAgentOptions.AllowedUserAgents.Any(u =>
                   currentRequest.UserAgent.Contains(u))
               && userConfiguration.PathsToAuthorize.Any(s => currentRequest.Path.StartsWithSegments(s));
    }

    /// <summary>
    /// If the user has allowed all user agents but has the only to only allow certain referrers, and the path matches
    /// </summary>
    /// <param name="currentRequest"></param>
    /// <param name="userConfiguration"></param>
    /// <returns></returns>
    private static bool AllowReferrersListAllUserAgents(CurrentRequest currentRequest,
        UserConfiguration userConfiguration)
    {
        return userConfiguration.UserAgentOptions.AllowAllUserAgents
            && userConfiguration.ReferrerOptions.AllowedReferrers.Any(u => currentRequest.Referrer.Contains(u))
            && userConfiguration.PathsToAuthorize.Any(s => currentRequest.Path.StartsWithSegments(s));
    }
    
    /// <summary>
    /// If the user has set a list of allowed referrers and a list of allowed user agents and the path matches
    /// </summary>
    /// <param name="currentRequest"></param>
    /// <param name="userConfiguration"></param>
    /// <returns></returns>
    private static bool AllowListReferrersListAllUserAgents(CurrentRequest currentRequest,
        UserConfiguration userConfiguration)
    {
        return userConfiguration.PathsToAuthorize.Any(s => currentRequest.Path.StartsWithSegments(s)
                && (userConfiguration.UserAgentOptions.AllowedUserAgents.Any(u => currentRequest.UserAgent.Contains(u))
                    || userConfiguration.ReferrerOptions.AllowedReferrers.Any(u => currentRequest.Referrer.Contains(u))
                    ));
    }
}