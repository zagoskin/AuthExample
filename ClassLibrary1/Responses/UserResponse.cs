using AuthExample.Infrastructure.Models;

namespace AuthExample.Domain.Responses;
public class UserResponse
{
    public UserResponse(User user)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        Role = user.Role is null ? null : new RoleResponse(user.Role);
    }
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public RoleResponse? Role { get; set; }
}
