using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventEase.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // ----------------- SYNC -----------------
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        List<T> GetAll();
        T? GetById(int id);
        void SaveChanges();

        // ----------------- ASYNC -----------------
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
