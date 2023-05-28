using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Customers.Infrastructure.Persistance.DbContexts;
internal class CustomersDbContext : DbContext
{
	private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

	public DbSet<Customer> Customers { get; set; }

	public CustomersDbContext(DbContextOptions<CustomersDbContext> dbContextOptions, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(dbContextOptions)
	{
		_auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("customers");
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}
