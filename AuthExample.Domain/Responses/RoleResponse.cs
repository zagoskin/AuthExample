using AuthExample.Infrastructure.Enums;
using AuthExample.Infrastructure.Models;

namespace AuthExample.Domain.Responses;
public class RoleResponse
{
    public RoleResponse(Role role)
    {
        Name = role.Name.ToString();
        Description = role.Description ?? string.Empty;
    }
    public string Name { get; set; }
    public string Description { get; set; }
}
