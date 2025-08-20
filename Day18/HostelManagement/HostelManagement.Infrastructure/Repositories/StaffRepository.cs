using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;

namespace HostelManagement.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly List<Staff> _staff = new();

        public Task<List<Staff>> GetAllAsync()
        {
            return Task.FromResult(_staff.ToList());
        }

        public Task<Staff?> GetByIdAsync(int id)
        {
            var staff = _staff.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(staff);
        }

        public Task AddAsync(Staff staff)
        {
            _staff.Add(staff);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Staff staff)
        {
            var existing = _staff.FirstOrDefault(s => s.Id == staff.Id);
            if (existing != null)
            {
                existing.Name = staff.Name;
                existing.Capacity = staff.Capacity;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var staff = _staff.FirstOrDefault(s => s.Id == id);
            if (staff != null)
                _staff.Remove(staff);

            return Task.CompletedTask;
        }
    }
}
