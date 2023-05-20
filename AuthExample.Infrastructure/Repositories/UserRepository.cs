using AuthExample.Infrastructure.Contexts;
using AuthExample.Infrastructure.Interfaces;
using AuthExample.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthExample.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly InMemoryDB _context;

    public UserRepository(InMemoryDB context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.Include(u => u.Role)
                                    .ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string userEmail)
    {
        return await _context.Users.Include(u => u.Role)
                                    .FirstOrDefaultAsync(u => u.Email.ToLower() == userEmail.ToLower());
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.Include(u => u.Role)
                                    .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.Users.Include(u => u.Role)
                                    .FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
    }

    public async Task SetRoleAsync(Guid id, Role role)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new Exception("User not found");
        user.Role = role;
        await _context.SaveChangesAsync();
    }
}
