using EventEase.Core.Entities;
using EventEase.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EventEase.Repository.Implementations
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly List<Registration> _registrations = new();
        private int _nextId = 1;

        public void Add(Registration entity)
        {
            entity.Id = _nextId++;
            entity.RegistrationDate = DateTime.Now;
            _registrations.Add(entity);
        }

        public void Update(Registration entity)
        {
            var existing = _registrations.FirstOrDefault(r => r.Id == entity.Id);
            if (existing != null)
            {
                existing.UserId = entity.UserId;
                existing.EventId = entity.EventId;
                
            }
        }

        public void Delete(int id)
        {
            var reg = _registrations.FirstOrDefault(r => r.Id == id);
            if (reg != null)
            {
                _registrations.Remove(reg);
            }
        }

        public List<Registration> GetAll()
        {
            return _registrations;
        }

        public Registration? GetById(int id)
        {
            return _registrations.FirstOrDefault(r => r.Id == id);
        }

        public void SaveChanges()
        {
            // No-op for in-memory list
        }
    }
}
