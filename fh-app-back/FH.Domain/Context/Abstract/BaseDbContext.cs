using FH.Domain.Context.Abstract;
using FH.Domain.Entities.Abstract;
using FH.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Qorp.Domain.Context.Abstract;

public abstract class BaseDbContext : DbContext
{
    /// <summary>
    /// Name migration table
    /// </summary>
    public const string MigrationsTableName = "_Migrations";
    
    /// <summary>
    /// Base constructor
    /// </summary>
    /// <param name="options">Options context parameters</param>
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }
    
    /// <summary>
    /// Action will do before DB save
    /// </summary>
    protected virtual void OnSavingChanges()
    {
        AddAuditInfo(this, DateTime.UtcNow);
    }

    /// <summary>
    /// Action will do after DB save
    /// </summary>
    protected virtual void OnSavedChanges()
    {
    }

    /// <inheritdoc/>
    public override int SaveChanges()
    {
        OnSavingChanges();
        var ret = base.SaveChanges();
        OnSavedChanges();
        return ret;
    }

    /// <inheritdoc/>
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnSavingChanges();
        var ret = base.SaveChanges(acceptAllChangesOnSuccess);
        OnSavedChanges();
        return ret;
    }

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnSavingChanges();
        var ret = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        OnSavedChanges();
        return ret;
    }

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnSavingChanges();
        var ret = await base.SaveChangesAsync(cancellationToken);
        OnSavedChanges();
        return ret;
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