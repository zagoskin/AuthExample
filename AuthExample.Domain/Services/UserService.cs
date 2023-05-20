using AuthExample.Domain.Exceptions;
using AuthExample.Domain.Interfaces;
using AuthExample.Domain.Responses;
using AuthExample.Infrastructure.Interfaces;

namespace AuthExample.Domain.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponse>> GetAllUsers()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserResponse(u)).ToList();
    }

    public async Task<UserResponse> GetUserById(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("User", id);
        return new UserResponse(user);
    }
}
