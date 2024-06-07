using EdgedGoods.Domain.Common.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EdgedGoods.Infrastructure.Interceptors;

public sealed class AuditInterceptor : SaveChangesInterceptor
{
    /// <summary>
    /// This method is called when saving changes to the database context.
    /// It updates the 'CreatedAt' and 'UpdatedAt' properties of any entity that implements the IAuditable interface.
    /// </summary>
    /// <param name="eventData">The event data containing information about the context.</param>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableEntities(eventData.Context);
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAuditableEntities(DbContext context)
    {
        var utcNow = DateTime.UtcNow;
        var auditableEntities = context.ChangeTracker
            .Entries<IAuditable>()
            .Where(e => e.Entity.IsAuditable)
            .ToList();
        
        // Loop through all entries in the context's change tracker
        foreach (var entry in auditableEntities)
        {
            
            // If the entity is in the 'Added' state, set its 'CreatedAt' property to the current UTC time
            if (entry.State == EntityState.Added)
            {
                SetCurrentPropertyValue(entry, nameof(IAuditable.CreatedAt), utcNow);
            }

            // If the entity is in the 'Modified' state, set its 'UpdatedAt' property to the current UTC time
            if (entry.State == EntityState.Modified)
            {
                SetCurrentPropertyValue(entry, nameof(IAuditable.UpdatedAt), utcNow);
            }
        }
    }
    
    private static void SetCurrentPropertyValue(
        EntityEntry entry,
        string propertyName,
        DateTime utcNow) =>
        entry.Property(propertyName).CurrentValue = utcNow;

}