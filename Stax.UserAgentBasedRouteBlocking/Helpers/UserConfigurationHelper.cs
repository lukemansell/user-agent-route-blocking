using Microsoft.Extensions.Configuration;
using Stax.UserAgentBasedRouteBlocking.Constants;
using Stax.UserAgentBasedRouteBlocking.Models;

namespace Stax.UserAgentBasedRouteBlocking.Helpers;

internal static class UserConfigurationHelper {
    internal static UserConfiguration GetUserConfiguration(IConfiguration configuration)
    {
        var allowAllUserAgents = configuration.GetValue<bool>(ConfigurationConstants.AllowAllUserAgents);
        var allowedUserAgents = configuration.GetSection(ConfigurationConstants.AllowedUserAgents)
            .GetChildren().Select(x => x.Value).ToArray();
        var allowAllReferrers = configuration.GetValue<bool>(ConfigurationConstants.AllowAllReferrers);
        var allowedReferrers = configuration.GetSection(ConfigurationConstants.AllowedReferrers)
            .GetChildren().Select(x => x.Value).ToArray();
        var pathsToAuthorize = configuration.GetSection(ConfigurationConstants.Paths)
            .GetChildren().Select(x => x.Value).ToArray();

        return new UserConfiguration()
        {
            ReferrerOptions = new ReferrerOptions()
            {
                AllowAllReferrers = allowAllReferrers,
                AllowedReferrers = allowedReferrers
            },
            UserAgentOptions = new UserAgentOptions()
            {
                AllowAllUserAgents = allowAllUserAgents,
                AllowedUserAgents = allowedUserAgents
            },
            PathsToAuthorize = pathsToAuthorize
        };
    }
    
}