using FH.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FH.Infrastructure.Context.Abstract;

/// <summary>
/// Extensions for work db context
/// </summary>
public static class DbContextExtensions
{
    public static IEnumerable<EntityEntry<T>> GetAddedOrEditedEntities<T>(this DbContext dbContext) where T : class
        => dbContext.ChangeTracker.Entries<T>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

    public static void DetachAll(this DbContext dbContext)
        => dbContext.ChangeTracker.Entries().ToArray().ForEach(entry => entry.State = EntityState.Detached);
}