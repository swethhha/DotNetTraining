using EventEase.Core.Entities;
using EventEase.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EventEase.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public void Add(User entity)
        {
            entity.Id = _nextId++;
            entity.Registrations = new List<Registration>();
            _users.Add(entity);
        }

        public void Update(User entity)
        {
            var existing = _users.FirstOrDefault(u => u.Id == entity.Id);
            if (existing != null)
            {
                existing.Name = entity.Name;
                existing.Email = entity.Email;
            }
        }

        public void Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User? GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public void SaveChanges()
        {
            // No-op for in-memory storage
        }
    }
}
