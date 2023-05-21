using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Persistance.DbContexts;
using GarageGenius.Modules.Users.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Users.Core.Persistance.Repositories;
internal class UserRepository : IUserRepository
{
    private readonly UsersDbContext _usersDbContext;

    public UserRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _usersDbContext.AddAsync(user, cancellationToken);
        await _usersDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeactivateUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        User? user = await _usersDbContext.Users
            .FirstOrDefaultAsync(user => user.UserId == id, cancellationToken);

        user?.VerifyUserState();

        if (user is not null)
        {
            user.Deactivate();
            await _usersDbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        User? user = await _usersDbContext.Users
            .AsQueryable()
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.UserId == id, cancellationToken);

        return user;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        User? user = await _usersDbContext.Users
            .AsQueryable()
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Email == email, cancellationToken);

        return user;
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _usersDbContext.Users.Update(user);
        await _usersDbContext.SaveChangesAsync(cancellationToken);
    }
}
