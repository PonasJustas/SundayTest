using Sunday.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunday.Repositories
{
    public interface IRepository<T> where T : IEntityId
    {
        Task<Guid> Add(T entity);
        Task AddBulk(IEnumerable<T> entities);
        Task Update(T entity);
        Task<List<T>> List();
        Task<T> Get(params object[] keys);
    }
}