using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Persistance.DbContexts;
using GarageGenius.Modules.Users.Core.Queries.GetUser;
using GarageGenius.Modules.Users.Core.Queries.GetUsers;
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
		await _usersDbContext.AddAsync(user, cancellationToken)
			.ConfigureAwait(false);

		await _usersDbContext.SaveChangesAsync(cancellationToken)
			.ConfigureAwait(false);
	}

	public async Task DeactivateUserAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var user = await _usersDbContext.Users
			.FirstOrDefaultAsync(user => user.UserId == id, cancellationToken)
			.ConfigureAwait(false);

		user?.VerifyUserState();

		if (user is not null)
		{
			user.Deactivate();
			await _usersDbContext.SaveChangesAsync(cancellationToken)
				.ConfigureAwait(false);
		}
	}

	public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var user = await _usersDbContext.Users
			.AsQueryable()
			.AsNoTracking()
			.Include(x => x.Role)
			.SingleOrDefaultAsync(x => x.UserId == id, cancellationToken)
			.ConfigureAwait(false);

		return user;
	}

	public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		var user = await _usersDbContext.Users
			.AsQueryable()
			.AsNoTracking()
			.Include(x => x.Role)
			.SingleOrDefaultAsync(x => x.Email == email, cancellationToken)
			.ConfigureAwait(false);

		return user;
	}

	public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
	{
		_usersDbContext.Users.Update(user);
        return _usersDbContext.SaveChangesAsync(cancellationToken);
    }

	public async Task<IReadOnlyList<GetUsersQueryDto>> GetUsersAsync(CancellationToken cancellationToken = default)
	{
		var users = await _usersDbContext.Users
			.AsQueryable()
			.AsNoTracking()
			.Include(user => user.Role)
			.Select(user => new GetUsersQueryDto(user.UserId, user.CustomerId, user.Role.Name, user.Email, user.State, user.Created))
			.ToListAsync(cancellationToken)
			.ConfigureAwait(false);

		return users;
	}
}
