using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barclays.ToDo.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Func<T, bool> expression);
        Task<T> GetAsync(int id);
        Task<T> Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}