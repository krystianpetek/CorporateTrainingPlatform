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

    public async Task AddAsync(Role role)
    {
        _usersDbContext.Roles.Add(role);
        await _usersDbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Role>> GetAllAsync()
    {
        return await _usersDbContext.Roles.ToListAsync();
    }

    public async Task<Role> GetAsync(string name)
    {
        return await _usersDbContext.Roles.SingleOrDefaultAsync(role => role.Name == name);
    }
}
