using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Users.Core.Persistance.DbContexts;
internal class UsersDbContext : DbContext
{
	private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
	public UsersDbContext(DbContextOptions<UsersDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
	{
		_auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Role> Roles { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("users");
		modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
		base.OnConfiguring(optionsBuilder);
	}
}
