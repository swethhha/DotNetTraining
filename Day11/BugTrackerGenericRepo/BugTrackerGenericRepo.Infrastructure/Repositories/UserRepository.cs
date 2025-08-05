using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTrackerGenericRepo.Core.Entities;
using BugTrackerGenericRepo.Core.Interfaces;

namespace BugTrackerGenericRepo.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public void Add(User entity)
        {
            _users.Add(entity);
        }

        public void Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
                _users.Remove(user);
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.UserId == id);
        }

        public void Update(User entity)
        {
            var existing = _users.FirstOrDefault(u => u.UserId == entity.UserId);
            if (existing != null)
            {
                existing.Username = entity.Username;
            }
        }
    }
}
