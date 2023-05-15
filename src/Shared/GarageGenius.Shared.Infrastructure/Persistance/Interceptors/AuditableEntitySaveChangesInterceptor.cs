using GarageGenius.Shared.Abstractions.Common;
using GarageGenius.Shared.Abstractions.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ISystemDateService _systemDateService;
    private readonly ICurrentUserService _currentUserService;

    public AuditableEntitySaveChangesInterceptor(ISystemDateService systemDateService, ICurrentUserService currentUserService) // TODO - Inject current user service
    {
        _systemDateService = systemDateService;
        _currentUserService = currentUserService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntity(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntity(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    private void UpdateEntity(DbContext? dbContext)
    {
        if (dbContext == null) return;

        foreach (EntityEntry<AuditableEntity> entry in dbContext.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUserService.UserId;
                entry.Entity.Created = _systemDateService.GetCurrentDate();
            }
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = _currentUserService.UserId;
                entry.Entity.LastModified = _systemDateService.GetCurrentDate();
            }
        }
    }
}
