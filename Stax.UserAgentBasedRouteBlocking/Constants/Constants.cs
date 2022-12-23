namespace Stax.UserAgentBasedRouteBlocking.Constants;

internal static class ConfigurationConstants
{
    private const string Authorization = "UserAgentBasedRouteBlocking";
    
    private const string Referrer = $"{Authorization}:Referrer";
    private const string UserAgent = $"{Authorization}:UserAgent";
    
    // Referrer
    internal const string AllowAllReferrers = $"{Referrer}:AllowAll";
    internal const string AllowedReferrers = $"{Referrer}:Allowed";

    // User Agents
    internal const string AllowAllUserAgents = $"{UserAgent}:AllowAll";
    internal const string AllowedUserAgents = $"{UserAgent}:Allowed";
    
    // Paths
    internal const string Paths = $"{Authorization}:Paths";
}