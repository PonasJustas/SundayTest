using Microsoft.EntityFrameworkCore;
using Sunday.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunday.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntityId
    {
        protected RepositoryBase(SundayContext context)
        {
            Context = context;
        }

        protected SundayContext Context { get; }
        protected DbSet<T> DbSet => Context.Set<T>();

        public virtual async Task<Guid> Add(T entity)
        {
            var insertedEntity = await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();

            return insertedEntity.Entity.Uuid;
        }

        public virtual async Task AddBulk(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
            await Context.SaveChangesAsync();
        }

        public virtual async Task Update(T entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> List()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<T> Get(params object[] keys)
        {
            try
            {
                return await DbSet.FindAsync(keys);
            }
            catch
            {
            }

            return null;
        }
    }
}
