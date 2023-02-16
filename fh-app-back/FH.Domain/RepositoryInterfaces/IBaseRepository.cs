using System.Linq.Expressions;

namespace FH.Domain.RepositoryInterfaces;

public interface IBaseRepository<TEntity> 
    where TEntity : class
{
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<TEntity?> GetOneAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);
    
    Task<IList<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);
    
    Task<TEntity> AddAsync(TEntity model, CancellationToken cancellationToken);
    
    Task AddRangeAsync(IList<TEntity> models, CancellationToken cancellationToken);

    Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellationToken);

    Task<TEntity> UpdatePartiallyAsync(TEntity model, IEnumerable<string> updatedProperties, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}