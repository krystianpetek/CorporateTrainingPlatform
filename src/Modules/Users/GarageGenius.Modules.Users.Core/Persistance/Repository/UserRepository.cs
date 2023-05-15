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

    public async Task DeactivateUserAsync(Guid id)
    {
        User? user = await _usersDbContext.Users
            .FirstOrDefaultAsync(user => user.Id == id);

        user?.VerifyUserState();

        if (user is not null)
        {
            user.Deactivate();
            await _usersDbContext.SaveChangesAsync();
        }
    }

    public async Task<User?> GetAsync(Guid id)
    {
        User? user = await _usersDbContext.Users
            .AsQueryable()
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id == id);

        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        User? user = await _usersDbContext.Users
            .AsQueryable()
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Email == email);

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _usersDbContext.Users.Update(user);
        await _usersDbContext.SaveChangesAsync();
    }
}
