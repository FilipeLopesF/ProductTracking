using System;
using _3DTrackingProducts.Api.Core.Repositories;
using System.Linq.Expressions;
using _3DTrackingProducts.Api.Data;
using Microsoft.EntityFrameworkCore;
using _3DTrackingProducts.Api.Resources;

namespace _3DTrackingProducts.Api.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).AnyAsync();
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> LastOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> item)
        {
            return await _context.Set<TEntity>().OrderBy(item).LastOrDefaultAsync(predicate);
        }

        public async Task Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task Remove(TEntity entity)
        {
            await Task.Run(() =>
                _context.Set<TEntity>().Remove(entity)
            );
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<int> Count()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
