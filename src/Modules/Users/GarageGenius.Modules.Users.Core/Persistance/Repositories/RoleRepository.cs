using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Persistance.DbContexts;
using GarageGenius.Modules.Users.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Users.Core.Persistance.Repositories;
internal class RoleRepository : IRoleRepository
{
	private readonly UsersDbContext _usersDbContext;

	public RoleRepository(UsersDbContext usersDbContext)
	{
		_usersDbContext = usersDbContext;
	}

	public async Task<IReadOnlyList<Role>> GetRolesAsync(CancellationToken cancellationToken = default)
	{
		IReadOnlyList<Role> roles = await _usersDbContext.Roles
			.ToListAsync(cancellationToken);

		return roles;
	}

	public async Task<Role?> GetAsync(string name, CancellationToken cancellationToken = default)
	{
		Role? role = await _usersDbContext.Roles
			.SingleOrDefaultAsync(role => role.Name == name, cancellationToken);

		return role;
	}

	public async Task AddAsync(Role role, CancellationToken cancellationToken = default)
	{
		await _usersDbContext.Roles
			.AddAsync(role, cancellationToken);

		await _usersDbContext.SaveChangesAsync(cancellationToken);
	}
}
