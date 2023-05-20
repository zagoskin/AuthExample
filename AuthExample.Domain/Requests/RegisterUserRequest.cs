using AuthExample.Infrastructure.Enums;

namespace AuthExample.Domain.Requests;
public class RegisterUserRequest : LoginUserRequest
{
    public new string UserName { get; set; } = null!;
    public new string Email { get; set; } = null!;
    public RoleType Role { get; set; }
}
