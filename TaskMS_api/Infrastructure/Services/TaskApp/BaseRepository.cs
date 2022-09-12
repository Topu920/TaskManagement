using Common.Service.Repositories;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task = System.Threading.Tasks.Task;

namespace Infrastructure.Services.TaskApp
{
    public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        protected readonly TaskManagementContext DbContext;

        protected BaseRepository(TaskManagementContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
            string? includeString,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                query = query.Include(includeString);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
            List<Expression<Func<TEntity, object>>>? includes,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                DbContext.Entry(entity).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        }
    }
}
