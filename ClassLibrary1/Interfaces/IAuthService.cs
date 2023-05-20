using AuthExample.Domain.Requests;
using AuthExample.Domain.Responses;

namespace AuthExample.Domain.Interfaces;
public interface IAuthService
{
    Task<UserResponse> Register(RegisterUserRequest request);
    Task<AuthResponse> Login(LoginUserRequest request);
}
