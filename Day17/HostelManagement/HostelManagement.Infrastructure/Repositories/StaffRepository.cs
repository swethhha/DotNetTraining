using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;

namespace HostelManagement.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly List<Staff> _staffs = new();
        private int _nextId = 1;

        public void Add(Staff entity)
        {
            entity.Id = _nextId++;
            _staffs.Add(entity);
        }

        public void Update(Staff entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Name = entity.Name;
                existing.Capacity = entity.Capacity;
                existing.Students = entity.Students;
            }
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing != null)
                _staffs.Remove(existing);
        }

        public Staff? GetById(int id) => _staffs.FirstOrDefault(s => s.Id == id);

        public List<Staff> GetAll() => _staffs;
    }
}
