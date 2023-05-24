using Microsoft.AspNetCore.Authentication;

namespace AuthExample.API.Auth;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public string ApiKey { get; set; }
}
