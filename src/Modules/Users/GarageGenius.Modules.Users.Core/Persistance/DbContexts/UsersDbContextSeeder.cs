using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Shared.Abstractions.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Users.Core.Persistance.DbContexts;
internal class UsersDbContextSeeder : IDbContextSeeder
{
    private readonly HashSet<string> _permissions = new()
    {
        "users","cars","clients"
    };

    private readonly UsersDbContext _usersDbContext;

    public UsersDbContextSeeder(UsersDbContext dbContext)
    {
        _usersDbContext = dbContext;
    }

    public async Task SeedDatabaseAsync()
    {
        if(await _usersDbContext.Roles.AnyAsync())
        {
            return;
        }
        await AddRolesAsync();
    }

    private async Task AddRolesAsync()
    {
        await _usersDbContext.Roles.AddAsync(new Role("admin", _permissions));
        await _usersDbContext.Roles.AddAsync(new Role("user", new List<string>()));
        
        await _usersDbContext.SaveChangesAsync();
    }
}
