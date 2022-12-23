namespace Stax.UserAgentBasedRouteBlocking.Models;

/// <summary>
/// User configuration model used to store values which the user has provided in their appsettings.json
/// </summary>
internal class UserConfiguration
{
    /// <summary>
    /// List of paths to apply the rules in <see cref="AllowedUserAgents"/> and <see cref="AllowedReferrers"/>. The path
    /// check checks against the start. Eg: for https://google.com/v1/api.json you could enter in <code>/v1/api.json</code>
    /// or alternatively <code>/v1</code> would also capture that URL as it checks the start.
    /// </summary>
    public string[]? PathsToAuthorize { get; set; }
    
    /// <summary>
    /// List of allowed user agents.
    /// </summary>
    public UserAgentOptions UserAgentOptions { get; set; }
    
    /// <summary>
    /// List of allowed HTTP referrers
    /// </summary>
    public ReferrerOptions ReferrerOptions { get; set; }
    
    
}