
using GarageGenius.Modules.Cars.Core.Entities;
using GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Cars.Infrastructure.Persistance.DbContexts;
internal class CarsDbContext : DbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DbSet<Car> Cars { get; set; }

    public CarsDbContext(DbContextOptions<CarsDbContext> dbContextOptions, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(dbContextOptions)
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
        modelBuilder.HasDefaultSchema("cars");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
