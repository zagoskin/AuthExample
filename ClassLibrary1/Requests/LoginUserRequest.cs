using AuthExample.Infrastructure.Enums;

namespace AuthExample.Domain.Requests;
public class LoginUserRequest
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; } = null!;
}
