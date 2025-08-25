using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Exceptions;
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
                RoomsManaged = s.Rooms.Count,
                RoomNumbers = s.Rooms.Select(r => r.RoomNumber).ToList()
            }).ToList();
        }

        public async Task<StaffResponseDTO?> GetStaffByIdAsync(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null)
                throw new NotFoundException($"Staff with ID {id} not found.");

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
            if (string.IsNullOrWhiteSpace(staffRequest.Name))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Name", new[] { "Staff name is required." } }
                });
            }

            if (staffRequest.Capacity <= 0)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Capacity", new[] { "Capacity must be greater than zero." } }
                });
            }

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
            if (staff == null)
                throw new NotFoundException($"Staff with ID {id} not found.");

            staff.Name = staffRequest.Name;
            staff.Capacity = staffRequest.Capacity;

            await _staffRepository.UpdateAsync(staff);
        }

        public async Task DeleteStaffAsync(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null)
                throw new NotFoundException($"Staff with ID {id} not found.");

            await _staffRepository.DeleteAsync(id);
        }
    }
}
