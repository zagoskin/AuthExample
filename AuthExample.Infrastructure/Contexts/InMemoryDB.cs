﻿using AuthExample.Infrastructure.Enums;
using AuthExample.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthExample.Infrastructure.Contexts;
public class InMemoryDB : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    
    public InMemoryDB(DbContextOptions<InMemoryDB> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("InMemoryDB");        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var adminRoleId = Guid.NewGuid();
        var guestRoleId = Guid.NewGuid();

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = adminRoleId, Name = RoleType.Admin },
            new Role { Id = guestRoleId, Name = RoleType.Guest });

        modelBuilder.Entity<User>().HasData(
            new User { RoleId = adminRoleId, UserName = "AdminExample", Email = "admin@example.com", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() },
            new User { RoleId = guestRoleId, UserName = "GuestExample", Email = "guest@example.com", PasswordHash = Array.Empty<byte>(), PasswordSalt = Array.Empty<byte>() });
    }
}
