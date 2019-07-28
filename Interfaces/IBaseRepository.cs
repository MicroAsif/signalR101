using signalR101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace signalR101.Interfaces
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        IQueryable<T> All();
        IQueryable<T> All(params Expression<Func<T, object>>[] includeProperties);
        T Find(Guid id);
        Task<T> FindAsync(Guid id);

        void Add(T entity);
        Task AddAsync(T entity);

        void AddRange(List<T> entities);
        Task AddAsyncRange(List<T> entites);

        void Delete(T entity);


        void DeleteRange(List<T> entites);
        Task DeleteAsyncRange(List<T> entites);

        void Update(T entity);

        void UpdateRange(List<T> entites);


        void SaveChanges();
        Task SaveChangesAsync();

    }
}
