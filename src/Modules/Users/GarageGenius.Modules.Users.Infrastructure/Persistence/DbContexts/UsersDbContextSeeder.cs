using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Authentication.PasswordManager;
using GarageGenius.Shared.Abstractions.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Users.Infrastructure.Persistence.DbContexts;
internal class UsersDbContextSeeder : IDbContextSeeder
{
	private readonly HashSet<string> _permissions = ["users", "vehicles", "notifications,customers"];

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

	private IEnumerable<User> _users =>
	[
		new User(new EmailAddress("administrator@garagegenius.com"), _passwordManager.Generate("garageGenius"),
			Roles.Administrator),
		new User(new EmailAddress("manager@garagegenius.com"), _passwordManager.Generate("garageGenius"),
			Roles.Manager),
		new User(new EmailAddress("employee@garagegenius.com"), _passwordManager.Generate("garageGenius"),
			Roles.Employee),
		new User(new EmailAddress("customer@garagegenius.com"), _passwordManager.Generate("garageGenius"),
			Roles.Customer)
	];

	private async Task AddRolesAsync()
	{
		await _usersDbContext.Roles.AddRangeAsync(_roles);
		await _usersDbContext.SaveChangesAsync();
	}

	private List<Role> _roles => new List<Role>()
	{
		new Role(Roles.Administrator, _permissions),
		new Role(Roles.Manager, _permissions),
		new Role(Roles.Employee, _permissions),
		new Role(Roles.Customer, new List<string>())
	};

	// TODO : Move to config file ?
}
