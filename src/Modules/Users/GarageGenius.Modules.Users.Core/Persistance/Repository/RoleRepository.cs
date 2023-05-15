using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Persistance.DbContexts;
using GarageGenius.Modules.Users.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Users.Core.Persistance.Repository;
internal class RoleRepository : IRoleRepository
{
    private readonly UsersDbContext _usersDbContext;

    public RoleRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<IReadOnlyList<Role>> GetRolesAsync()
    {
        IReadOnlyList<Role> roles = await _usersDbContext.Roles
            .ToListAsync();

        return roles;
    }

    public async Task<Role?> GetAsync(string name)
    {
        Role? role = await _usersDbContext.Roles
            .SingleOrDefaultAsync(role => role.Name == name);
        
        return role;
    }

    public async Task AddAsync(Role role)
    {
        await _usersDbContext.Roles
            .AddAsync(role);
        
        await _usersDbContext.SaveChangesAsync();
    }
}
