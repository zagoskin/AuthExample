using AuthExample.Infrastructure.Contexts;
using AuthExample.Infrastructure.Enums;
using AuthExample.Infrastructure.Interfaces;
using AuthExample.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthExample.Infrastructure.Repositories;
public class RoleRepository : IRoleRepository
{
    private readonly InMemoryDB _context;

    public RoleRepository(InMemoryDB context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public async Task<List<Role>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task<Role?> GetByTypeAsync(RoleType roleType)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleType);
    }
}
