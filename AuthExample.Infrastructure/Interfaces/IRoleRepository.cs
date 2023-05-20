using AuthExample.Infrastructure.Enums;
using AuthExample.Infrastructure.Models;

namespace AuthExample.Infrastructure.Interfaces;
public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role?> GetByTypeAsync(RoleType roleType);
}
