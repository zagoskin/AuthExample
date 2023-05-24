using AuthExample.API.Auth;
using Microsoft.AspNetCore.Authentication;

namespace AuthExample.API.Extensions;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddApiKeySupport(this AuthenticationBuilder builder)
    {
        return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>("ApiKey", options => 
        {
            options.ApiKey = "toto";
        });
    }
}
