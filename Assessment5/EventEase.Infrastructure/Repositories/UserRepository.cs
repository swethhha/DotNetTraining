using EventEase.Core.Entities;
using EventEase.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventEase.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        // ----------------- SYNC -----------------
        public void Add(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public void Update(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing != null)
            {
                existing.Name = user.Name;
                existing.Email = user.Email;
            }
        }

        public void Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null) _users.Remove(user);
        }

        public List<User> GetAll() => _users;

        public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public void SaveChanges() { /* no-op for in-memory */ }

        // ----------------- ASYNC -----------------
        public Task AddAsync(User user)
        {
            Add(user);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(User user)
        {
            Update(user);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public Task<List<User>> GetAllAsync() => Task.FromResult(GetAll());

        public Task<User?> GetByIdAsync(int id) => Task.FromResult(GetById(id));

        public Task SaveChangesAsync()
        {
            SaveChanges();
            return Task.CompletedTask;
        }
    }
}
