using FH.Domain.Entities.Abstract;
using FH.Infrastructure.Context.Abstract;
using FH.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FH.Infrastructure.Context.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AddAuditInfo(eventData.Context!, DateTime.UtcNow);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        AddAuditInfo(eventData.Context!, DateTime.UtcNow);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    public void AddAuditInfo(DbContext dbContext, DateTime utcNow)
    {
        var entriesWithDateAudit = dbContext.GetAddedOrEditedEntities<IAuditDateInfo>();
        entriesWithDateAudit.ForEach(entry =>
        {
            var i = entry.Entity;
            if (entry.State == EntityState.Added)
            {
                i.Created = utcNow;
            }

            i.Updated = utcNow;
        });
    }
}