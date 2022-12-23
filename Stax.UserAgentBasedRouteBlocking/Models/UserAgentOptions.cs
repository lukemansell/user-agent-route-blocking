namespace Stax.UserAgentBasedRouteBlocking.Models;

internal class UserAgentOptions
{
    internal bool AllowAllUserAgents { get; set; }
    
    internal string[]? AllowedUserAgents { get; set; }
}