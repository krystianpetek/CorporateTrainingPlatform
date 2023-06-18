using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Shared.Abstractions.Authentication.PasswordManager;
using GarageGenius.Shared.Abstractions.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Users.Core.Persistance.DbContexts;
internal class UsersDbContextSeeder : IDbContextSeeder
{
	private readonly HashSet<string> _permissions = new()
	{
		"users","vehicles","notifications,customers"
	};

	private readonly UsersDbContext _usersDbContext;
	private readonly IPasswordManager _passwordManager;

	public UsersDbContextSeeder(UsersDbContext dbContext, IPasswordManager passwordManager)
	{
		_usersDbContext = dbContext;
		_passwordManager = passwordManager;
	}

	public async Task SeedDatabaseAsync()
	{
		if (_usersDbContext.Database.IsRelational())
		{
			if (_usersDbContext.Database.GetPendingMigrations().Any())
				await _usersDbContext.Database.MigrateAsync();
		}
		else
		{
			_usersDbContext.Database.EnsureCreated();
		}

		if (await _usersDbContext.Roles.AnyAsync())
		{
			return;
		}
		Task rolesSeed = AddRolesAsync();
		Task usersSeed = AddUsersAsync();

		await Task.WhenAll(rolesSeed, usersSeed);
	}

	private async Task AddUsersAsync()
	{
		await _usersDbContext.Users.AddRangeAsync(_users);
		await _usersDbContext.SaveChangesAsync();
	}

	private List<User> _users => new List<User>()
	{
		new User(new ValueObjects.EmailAddress("admin@garagegenius.com"), _passwordManager.Generate("garageGenius"), "Administrator")
	};

	private async Task AddRolesAsync()
	{
		await _usersDbContext.Roles.AddRangeAsync(_roles);
		await _usersDbContext.SaveChangesAsync();
	}

	private List<Role> _roles => new List<Role>()
	{
		new Role("administrator", _permissions),
		new Role("manager", _permissions),
		new Role("employee", _permissions),
		new Role("customer", new List<string>())
	};

	// TODO : Move to config file ?
}
