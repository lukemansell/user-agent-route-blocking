# User Agent Based Route Blocking

[![Version](https://img.shields.io/nuget/vpre/stax.useragentbasedrouteblocking.svg)](https://www.nuget.org/packages/stax.autodependencyinjectionregistration)
[![Downloads](https://img.shields.io/nuget/dt/stax.useragentbasedrouteblocking.svg)](https://www.nuget.org/packages/stax.autodependencyinjectionregistration)
---
## Summary

This [NuGet library](https://www.nuget.org/packages/Stax.UserAgentBasedRouteBlocking/) allows you to block certain routes based on whether or not they have a certain user agent. It is a very simple way of doing API based authorisation. It also has the ability to allow certain referrers through, which can help enable your swagger to work for example, or help limit user agents coming from a certain site.

An example might be you want to limit anyone from hitting https://api.example.com/v1/create-user.json unless they have the user agent `EXAMPLE.COM-FRONTEND` which your front end client sends. You could then also only allow requests with that user agent from `https://example.com` and also `https://api.example.com` (the latter which would allow your swagger to work for example).

## How to use

Configuration is set through your appsettings.json (or you can inject as environment variables).

### Some how to use notes
* The path check does a "starts with" check. Meaning to protect `https://api.example.com/v1` you would enter in `UserAgentBasedRouteBlocking:Paths` "/v1". This also means it acts like a wildcard meaning anything which starts with "/v1" is protected. Eg: `/v1/create.json`, `/v1/upload.json` etc
* User agent does a contains check, meaning `Mozilla` would cover `Mozilla 5.0 - Linux` for example.
* Referrer does a "contains" check, meaning `https://example.com/a-page` would be covered by setting `https://example.com` in your config

In your appsettings.json you need a structure of:

```json
{
  "UserAgentBasedRouteBlocking": {
    "Paths": [
      LIST
      OF
      PATHS
      TO
      CHECK
    ],
    "Referrer": {
      "AllowAll": bool - optional defaults to false,
      "Allowed": [
        LIST
        OF
        ALLOWED
        REFERRERS
      ]
    },
    "UserAgent": {
      "AllowAll": bool - optional defaults to false,
      "Allowed": [
        LIST
        OF
        ALLOWED
        USER
        AGENTS
      ]
    }
  }
}
```

Below is an example which:
* Checks the paths, `/v1/hello-world.json`, `/v1/diagnostics` and `/v2`
  * It is important to note that this checks the start of the route, so for example `/v2/api.json` would be covered by `/v2`
* Allows requests from `https://google.com`, `https://facebook.com`, `https://apiconsumer.com`
* Allows requests from user agents `API Consumer User Agent` and `API Consumer2 User Agent`

```json
{
  "UserAgentBasedRouteBlocking": {
    "Paths": [
      "/v1/hello-world.json",
      "/v1/diagnostics",
      "/v2/"
    ],
    "Referrer": {
      "AllowAll": false,
      "Allowed": [
        "https://google.com",
        "https://facebook.com",
        "https://apiconsumer.com"
      ]
    },
    "UserAgent": {
      "AllowAll": false,
      "Allowed": [
        "API Consumer User Agent",
        "API Consumer2 User Agent"
      ]
    }
  }
}

```

As you have provided allowed lists for user agents and referrers, you do not need to set the AllowAll property for each.