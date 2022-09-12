using System.Linq.Expressions;

namespace Common.Service.Repositories
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
                                        string? includeString,
                                        bool disableTracking = true);
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
                                        List<Expression<Func<TEntity, object>>>? includes,
                                        bool disableTracking = true);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> GetByIdAsync(string id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
