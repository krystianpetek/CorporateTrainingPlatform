using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Reservations.Infrastructure.Persistance.DbContexts;
internal class ReservationsDbContext : DbContext
{
	private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

	public DbSet<Reservation> Reservations { get; set; }
	public DbSet<ReservationHistory> ReservationHistories { get; set; }

	public ReservationsDbContext(DbContextOptions<ReservationsDbContext> dbContextOptions, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(dbContextOptions)
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
		modelBuilder.HasDefaultSchema("reservations");
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}