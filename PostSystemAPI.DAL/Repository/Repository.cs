using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly PostSystemContext context;
        private readonly DbSet<TEntity> dbEntities;

        public Repository(PostSystemContext context)
        {
            this.context = context;
            dbEntities = this.context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
            => (await dbEntities.AddAsync(entity)).Entity;

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
            => dbEntities.AddRangeAsync(entities);

        public void Delete(TEntity entity)
            => dbEntities.Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = await context.Set<TEntity>().AsNoTracking().ToListAsync();
            return query;
        }

        public ValueTask<TEntity> GetByIdAsync(int id)
            => dbEntities.FindAsync(id);

        public async Task<TEntity> UpdateAsync(TEntity entity)
            => await Task.Run(() => dbEntities.Update(entity).Entity);

        public async Task<int> SaveChangesAsync()
            => await context.SaveChangesAsync();
        
    }
}
