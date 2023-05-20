using Microsoft.Extensions.DependencyInjection;
using AuthExample.Infrastructure;
using AuthExample.Domain.Interfaces;
using AuthExample.Domain.Services;

namespace AuthExample.Domain;
public static class DomainSetup
{
    public static void ConfigureDomain(this IServiceCollection services)
    {
        services.ConfigureInfrastructure();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
    }
}
