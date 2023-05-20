using AuthExample.Domain.Responses;

namespace AuthExample.Domain.Interfaces;
public interface IUserService
{
    Task<UserResponse> GetUserById(Guid id);
    Task<List<UserResponse>> GetAllUsers();
}
