using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;

namespace HostelManagement.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<List<StaffResponseDTO>> GetAllStaffAsync()
        {
            var staffList = await _staffRepository.GetAllAsync();
            return staffList.Select(s => new StaffResponseDTO
            {
                Id = s.Id,
                Name = s.Name,
                Capacity = s.Capacity,
                RoomsManaged = s.Rooms.Count, // assuming navigation property Rooms in Staff
                RoomNumbers = s.Rooms.Select(r => r.RoomNumber).ToList()
            }).ToList();
        }

        public async Task<StaffResponseDTO?> GetStaffByIdAsync(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null) return null;

            return new StaffResponseDTO
            {
                Id = staff.Id,
                Name = staff.Name,
                Capacity = staff.Capacity,
                RoomsManaged = staff.Rooms.Count,
                RoomNumbers = staff.Rooms.Select(r => r.RoomNumber).ToList()
            };
        }

        public async Task AddStaffAsync(StaffRequestDTO staffRequest)
        {
            var staff = new Staff
            {
                Name = staffRequest.Name,
                Capacity = staffRequest.Capacity
            };

            await _staffRepository.AddAsync(staff);
        }

        public async Task UpdateStaffAsync(int id, StaffRequestDTO staffRequest)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff != null)
            {
                staff.Name = staffRequest.Name;
                staff.Capacity = staffRequest.Capacity;

                await _staffRepository.UpdateAsync(staff);
            }
        }

        public async Task DeleteStaffAsync(int id)
        {
            await _staffRepository.DeleteAsync(id);
        }
    }
}
