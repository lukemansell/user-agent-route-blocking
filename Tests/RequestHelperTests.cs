using Stax.UserAgentBasedRouteBlocking.Helpers;
using Stax.UserAgentBasedRouteBlocking.Models;

namespace Tests;

public class RequestHelperTests
{
    private readonly CurrentRequest _currentRequest;

    public RequestHelperTests()
    {
        _currentRequest = new CurrentRequest()
        {
            Path = "/v1/api.json",
            Referrer = "https://api.com",
            UserAgent = "API-client-token"
        };
    }
    
    [Fact]
    public void ShouldAllowRequest_ReturnsTrue_When_AllowAllIsSetForReferrerAndUserAgent()
    {
        // arrange
        var userConfiguration = new UserConfiguration()
        {
            PathsToAuthorize = new[]
            {
                "/v1/api.json",
                "/v1/helloworld.json"
            },
            ReferrerOptions = new ReferrerOptions()
            {
                AllowAllReferrers = true
            },
            UserAgentOptions = new UserAgentOptions()
            {
                AllowAllUserAgents = true
            }
        };
        // act
        var result = RequestHelper.ShouldAllowRequest(_currentRequest, userConfiguration);
        
        // assert
        Assert.True(result);
    }
    
    [Fact]
    public void ShouldAllowRequest_ReturnsTrue_When_AValidListOfUserAgentsAndReferrersIsSupplied()
    {
        // arrange
        var userConfiguration = new UserConfiguration()
        {
            PathsToAuthorize = new[]
            {
                "/v1/api.json",
                "/v1/helloworld.json"
            },
            ReferrerOptions = new ReferrerOptions()
            {
                AllowedReferrers = new []
                {
                    "https://api.com"
                }
            },
            UserAgentOptions = new UserAgentOptions()
            {
                AllowedUserAgents = new []
                {
                    "API-client-token"
                }
            }
        };
        // act
        var result = RequestHelper.ShouldAllowRequest(_currentRequest, userConfiguration);
        
        // assert
        Assert.True(result);
    }
    
    [Fact]
    public void ShouldAllowRequest_ReturnsTrue_When_AValidListOfUserAgentsAndInvalidListOfReferrersIsSupplied()
    {
        // arrange
        var userConfiguration = new UserConfiguration()
        {
            PathsToAuthorize = new[]
            {
                "/v1/api.json",
                "/v1/helloworld.json"
            },
            ReferrerOptions = new ReferrerOptions()
            {
                AllowedReferrers = new []
                {
                    "https://notanapi.com"
                }
            },
            UserAgentOptions = new UserAgentOptions()
            {
                AllowedUserAgents = new []
                {
                    "API-client-token"
                }
            }
        };
        // act
        var result = RequestHelper.ShouldAllowRequest(_currentRequest, userConfiguration);
        
        // assert
        Assert.True(result);
    }

    [Fact]
    public void ShouldAllowRequest_ReturnsTrue_When_AnInvalidListOfUserAgentsAndValidListOfReferrersIsSupplied()
    {
        // arrange
        var userConfiguration = new UserConfiguration()
        {
            PathsToAuthorize = new[]
            {
                "/v1/api.json",
                "/v1/helloworld.json"
            },
            ReferrerOptions = new ReferrerOptions()
            {
                AllowedReferrers = new []
                {
                    "https://api.com"
                }
            },
            UserAgentOptions = new UserAgentOptions()
            {
                AllowedUserAgents = new []
                {
                    "API2-client-token"
                }
            }
        };
        // act
        var result = RequestHelper.ShouldAllowRequest(_currentRequest, userConfiguration);
        
        // assert
        Assert.True(result);
    }
    
    [Fact]
    public void ShouldAllowRequest_ReturnsFalse_When_AnInvalidListOfUserAgentsAndInvalidListOfReferrersIsSupplied()
    {
        // arrange
        var userConfiguration = new UserConfiguration()
        {
            PathsToAuthorize = new[]
            {
                "/v1/api.json",
                "/v1/helloworld.json"
            },
            ReferrerOptions = new ReferrerOptions()
            {
                AllowedReferrers = new []
                {
                    "https://FAIL.com"
                }
            },
            UserAgentOptions = new UserAgentOptions()
            {
                AllowedUserAgents = new []
                {
                    "FAIL"
                }
            }
        };
        // act
        var result = RequestHelper.ShouldAllowRequest(_currentRequest, userConfiguration);
        
        // assert
        Assert.False(result);
    }
}