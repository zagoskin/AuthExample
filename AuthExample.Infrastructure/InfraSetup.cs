using AuthExample.Infrastructure.Contexts;
using AuthExample.Infrastructure.Interfaces;
using AuthExample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthExample.Infrastructure;
public static class InfraSetup
{
    public static void ConfigureInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<InMemoryDB>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDB");
        });

        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
