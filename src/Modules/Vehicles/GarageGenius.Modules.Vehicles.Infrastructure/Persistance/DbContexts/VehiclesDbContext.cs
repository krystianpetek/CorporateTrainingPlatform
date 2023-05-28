using GarageGenius.Modules.Vehicles.Core.Entities;
using GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Vehicles.Infrastructure.Persistance.DbContexts;
internal class VehiclesDbContext : DbContext
{
	private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

	public DbSet<Vehicle> Vehicles { get; set; }

	public VehiclesDbContext(DbContextOptions<VehiclesDbContext> dbContextOptions, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(dbContextOptions)
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
		modelBuilder.HasDefaultSchema("vehicles");
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}
