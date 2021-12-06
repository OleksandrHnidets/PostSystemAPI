using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate =null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<bool> SaveChangesAsync();
    }
}
