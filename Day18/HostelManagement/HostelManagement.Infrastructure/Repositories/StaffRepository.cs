using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using HostelManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace HostelManagement.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly AppDbContext _context;

        public StaffRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Staff>> GetAllAsync()
        {
            return await _context.Staffs.ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(int id)
        {
            return await _context.Staffs.FindAsync(id);
        }

        public async Task AddAsync(Staff staff)
        {
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Staff staff)
        {
            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();
            }
        }
    }
}
