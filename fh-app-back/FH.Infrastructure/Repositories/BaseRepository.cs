using System.Linq.Expressions;
using FH.Domain.Exceptions;
using FH.Domain.RepositoryInterfaces;
using FH.Infrastructure.Context;
using FH.Infrastructure.Context.Abstract;
using FH.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FH.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
    where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;
    
    /// <summary>
    /// Logger
    /// </summary>
    protected ILogger Logger { get; }
    

    /// <summary>
    /// Base repository constructor
    /// </summary>
    /// <param name="dbContext">Fabric data context</param>
    /// <param name="logger">Logger</param>
    public BaseRepository(ApplicationDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        Logger = logger;
    }
    
    /// <summary>
    /// Work with context db (with save) and user with catch exception
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    /// <param name="func">Action inside context</param>
    /// <param name="cancellationToken">Cancel token</param>
    /// <returns>Result</returns>
    protected Task<T> SaveChangesAndHandleExceptionAsync<T>(Func<ApplicationDbContext, CancellationToken, Task<Func<T>>> func, CancellationToken cancellationToken)
        => HandleAsync(async (dbContext, cToken) =>
        {
            var afterSaveFunc = await func(dbContext, cToken);
            await dbContext.SaveChangesAsync(cToken);
            return afterSaveFunc();
        }, cancellationToken);


    /// <summary>
    /// Work with context db with catch exception
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    /// <param name="func">Action inside context</param>
    /// <param name="cancellationToken">Cancel token</param>
    /// <returns>Result</returns>
    protected async Task<T> HandleAsync<T>(
        Func<ApplicationDbContext, CancellationToken, Task<T>> func, 
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await func(_dbContext, cancellationToken);
            return result;
        }
        catch (DbUpdateException e)
        {
            Logger.LogError(e, "DbUpdateException error");
            throw new FhException(e.Message, e);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Exception error");
            throw;
        }
    }

    public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        => HandleAsync((dbContext, cToken) => 
            dbContext.Set<TEntity>().AsQueryable().AnyAsync(predicate, cToken), cancellationToken);

    public virtual Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        => HandleAsync(async (dbContext, cToken) =>
        {
            var query = dbContext.Set<TEntity>().AsQueryable().AsNoTracking();

            var entity = await query.FirstOrDefaultAsync(predicate, cToken);

            if (entity is null)
            {
                return default;
            }

            return entity;
        }, cancellationToken);
    
    public virtual Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken)
        => HandleAsync(async (dbContext, cToken) =>
        {
            var query = dbContext.Set<TEntity>().AsQueryable().AsNoTracking();

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }
            
           return (IList<TEntity>)await query.ToListAsync(cToken);
        }, cancellationToken);

    public virtual Task<TEntity> AddAsync(TEntity model, CancellationToken cancellationToken)
        => SaveChangesAndHandleExceptionAsync<TEntity>(async (dbContext, cToken) =>
        {
            var result = await dbContext.Set<TEntity>().AddAsync(model, cToken);
            return () =>
            {
                dbContext.DetachAll();
                return result.Entity;
            };
        }, cancellationToken);

    public virtual Task AddRangeAsync(IList<TEntity> models, CancellationToken cancellationToken)
        => SaveChangesAndHandleExceptionAsync<Task>(async (dbContext, cToken) =>
        {
            await dbContext.Set<TEntity>().AddRangeAsync(models, cToken);
            return () =>
            {
                dbContext.DetachAll();
                return Task.CompletedTask;
            };
        }, cancellationToken);

    public virtual Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellationToken)
        => SaveChangesAndHandleExceptionAsync<TEntity>(async (dbContext, cToken) =>
        {
            dbContext.Entry(model).State = EntityState.Modified;
            var result = dbContext.Set<TEntity>().Update(model);
            
            return () =>
            {
                dbContext.DetachAll();
                return result.Entity;
            };
        }, cancellationToken);

    public virtual Task<TEntity> UpdatePartiallyAsync(TEntity model, IEnumerable<string> updatedProperties, CancellationToken cancellationToken)
        => SaveChangesAndHandleExceptionAsync<TEntity>(async (dbContext, cToken) =>
        {
            var keyNames = dbContext.Model
                .FindEntityType(typeof(TEntity))
                .FindPrimaryKey().Properties
                .Select(x => x.Name);
            
            updatedProperties.ForEach(item =>
            {
                if (!keyNames.Contains(item))
                {
                    dbContext.Entry(model).Property(item).IsModified = true;
                }
            });
            
            return () => model;
        }, cancellationToken);

    public virtual Task<bool> DeleteAsync(TEntity model, CancellationToken cancellationToken)
        => SaveChangesAndHandleExceptionAsync<bool>(async (dbContext, cToken) =>
        {
            dbContext.Set<TEntity>().Remove(model);
            return () => true;
        }, cancellationToken);

}