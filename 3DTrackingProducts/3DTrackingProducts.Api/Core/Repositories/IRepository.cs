using System;
using System.Linq.Expressions;

namespace _3DTrackingProducts.Api.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);

        Task<int> Count();

        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAll();

        Task Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<bool> Save();
    }
}

