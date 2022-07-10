using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PostSystemAPI.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected PostSystemContext _context;
        protected DbSet<TEntity> DbSet { get; }
        public IQueryable<TEntity> Entities { get; }

        public Repository(PostSystemContext context)
        {
            this._context = context;
            DbSet = this._context.Set<TEntity>();
            Entities = DbSet;
        }

        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
            => await _context.Set<TEntity>().AddRangeAsync(entities);

        public void Delete(TEntity entity)
            => _context.Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
                => await GetQuery(predicate, include).ToListAsync();

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
                => await GetQuery(predicate, include).FirstAsync();

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public async Task<bool> SaveChangesAsync()
            => await _context.SaveChangesAsync() >=0;

        private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            //var query = _context.Set<TEntity>().AsQueryable();
            var query = _context.Set<TEntity>().AsQueryable();
            if(include != null)
            {
                query = include(query);
            }
            if(predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }
        
    }
}
