using System;
using System.Collections.Generic;
using System.Linq;
using BugTracker.Core.Entities;
using BugTracker.Core.Interfaces;

namespace BugTracker.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public void Add(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public void Update(User updatedUser)
        {
            var existing = GetById(updatedUser.Id);
            if (existing != null)
            {
                existing.Name = updatedUser.Name;
            }
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetAll()
        {
            return _users;
        }
    }
}
