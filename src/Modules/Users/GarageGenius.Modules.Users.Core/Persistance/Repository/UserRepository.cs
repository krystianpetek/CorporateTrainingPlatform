using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Persistance.DbContexts;
using GarageGenius.Modules.Users.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Users.Core.Persistance.Repository;
internal class UserRepository : IUserRepository
{
    private readonly UsersDbContext _usersDbContext;

    public UserRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task AddAsync(User user)
    {
        await _usersDbContext.AddAsync(user);
        await _usersDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        User? user = await _usersDbContext.Users.FindAsync(id);
        if (user is not null)
        {
            user.Deactivate();
            await _usersDbContext.SaveChangesAsync();
        }
    }

    public Task<User> GetAsync(Guid id)
    {
        return _usersDbContext.Users
            .AsQueryable()
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<User> GetByEmailAsync(string email)
    {
        return _usersDbContext.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task UpdateAsync(User user)
    {
        _usersDbContext.Update(user);
        await _usersDbContext.SaveChangesAsync();
    }
}
