using EventEase.Core.Entities;
using EventEase.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventEase.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly List<Event> _events = new();
        private int _nextId = 1;

        // ----------------- SYNC -----------------
        public void Add(Event ev)
        {
            ev.Id = _nextId++;
            _events.Add(ev);
        }

        public void Update(Event ev)
        {
            var existing = _events.FirstOrDefault(e => e.Id == ev.Id);
            if (existing != null)
            {
                existing.Title = ev.Title;
                existing.Description = ev.Description;
                existing.Location = ev.Location;
                existing.Date = ev.Date;
            }
        }

        public void Delete(int id)
        {
            var ev = _events.FirstOrDefault(e => e.Id == id);
            if (ev != null) _events.Remove(ev);
        }

        public List<Event> GetAll() => _events;

        public Event? GetById(int id) => _events.FirstOrDefault(e => e.Id == id);

        public void SaveChanges() { /* no-op for in-memory */ }

        // ----------------- ASYNC -----------------
        public Task AddAsync(Event ev)
        {
            Add(ev);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Event ev)
        {
            Update(ev);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public Task<List<Event>> GetAllAsync() => Task.FromResult(GetAll());

        public Task<Event?> GetByIdAsync(int id) => Task.FromResult(GetById(id));

        public Task SaveChangesAsync()
        {
            SaveChanges();
            return Task.CompletedTask;
        }
    }
}
