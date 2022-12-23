namespace Stax.UserAgentBasedRouteBlocking.Models;

internal class ReferrerOptions
{
    internal bool AllowAllReferrers { get; set; }
    
    internal string[] AllowedReferrers { get; set; }
}