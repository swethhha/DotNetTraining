using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HostelManagement.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly List<Staff> _staffMembers = new();
        private int _nextId = 1;

        public void Add(Staff entity)
        {
            entity.Id = _nextId++;
            _staffMembers.Add(entity);
        }

        public void Update(Staff entity)
        {
            var existing = GetById(entity.Id);
            if (existing == null) return;
            existing.Name = entity.Name;
            existing.Rooms = entity.Rooms;
        }

        public void Delete(int id)
        {
            var s = GetById(id);
            if (s != null) _staffMembers.Remove(s);
        }

        public Staff? GetById(int id) => _staffMembers.FirstOrDefault(s => s.Id == id);

        public List<Staff> GetAll() => _staffMembers;
    }
}
