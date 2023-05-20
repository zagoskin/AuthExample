using AuthExample.Infrastructure.Models;

namespace AuthExample.Infrastructure.Interfaces;
public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName);
    Task<User?> GetByEmailAsync(string userEmail);
    Task<User> CreateAsync(User user);
    Task SetRoleAsync(Guid id, Role role);
}
