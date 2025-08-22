using EventEase.Core.Entities;
using EventEase.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventEase.Infrastructure.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly List<Registration> _registrations = new();
        private int _nextId = 1;

        // ----------------- SYNC -----------------
        public void Add(Registration reg)
        {
            reg.Id = _nextId++;
            _registrations.Add(reg);
        }

        public void Update(Registration reg)
        {
            var existing = _registrations.FirstOrDefault(r => r.Id == reg.Id);
            if (existing != null)
            {
                existing.UserId = reg.UserId;
                existing.EventId = reg.EventId;
            }
        }

        public void Delete(int id)
        {
            var reg = _registrations.FirstOrDefault(r => r.Id == id);
            if (reg != null) _registrations.Remove(reg);
        }

        public List<Registration> GetAll() => _registrations;

        public Registration? GetById(int id) => _registrations.FirstOrDefault(r => r.Id == id);

        public void SaveChanges() { /* no-op for in-memory */ }

        // ----------------- ASYNC -----------------
        public Task AddAsync(Registration reg)
        {
            Add(reg);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Registration reg)
        {
            Update(reg);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public Task<List<Registration>> GetAllAsync() => Task.FromResult(GetAll());

        public Task<Registration?> GetByIdAsync(int id) => Task.FromResult(GetById(id));

        public Task SaveChangesAsync()
        {
            SaveChanges();
            return Task.CompletedTask;
        }
    }
}
