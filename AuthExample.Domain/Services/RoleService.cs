using AuthExample.Domain.Interfaces;
using AuthExample.Domain.Responses;
using AuthExample.Infrastructure.Interfaces;

namespace AuthExample.Domain.Services;
public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<RoleResponse>> GetRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(r => new RoleResponse(r)).ToList();
    }
}
