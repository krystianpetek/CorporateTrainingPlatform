using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Shared.Abstractions.Persistance;
using Microsoft.EntityFrameworkCore;
 
namespace GarageGenius.Modules.Users.Core.Persistance.DbContexts;
internal class UsersDbContextSeeder : IDbContextSeeder
{
    private readonly HashSet<string> _permissions = new()
    {
        "users","cars","notifications,customers"
    };

    private readonly UsersDbContext _usersDbContext;

    public UsersDbContextSeeder(UsersDbContext dbContext)
    {
        _usersDbContext = dbContext;
    }

    public async Task SeedDatabaseAsync()
    {
        if (_usersDbContext.Database.GetPendingMigrations().Any())
            await _usersDbContext.Database.MigrateAsync();

        if (await _usersDbContext.Roles.AnyAsync())
        {
            return;
        }
        await AddRolesAsync();
    }

    private async Task AddRolesAsync()
    {
        await _usersDbContext.Roles.AddRangeAsync(_roles);
        await _usersDbContext.SaveChangesAsync();
    }

    private List<Role> _roles => new List<Role>() // TODO : Move to config file ?
    {
        new Role("Administrator", _permissions),
        new Role("Manager", _permissions),
        new Role("Employee", _permissions),
        new Role("Customer", new List<string>())
    };
}
