using AuthExample.Domain.Responses;

namespace AuthExample.Domain.Interfaces;
public interface IRoleService
{
    Task<List<RoleResponse>> GetRolesAsync();
}
