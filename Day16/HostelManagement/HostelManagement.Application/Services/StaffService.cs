using HostelManagement.Core.DTOs;
using HostelManagement.Core.DTOs.HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using System.Linq;

namespace HostelManagement.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public void AddStaff(StaffRequestDTO staffDto)
        {
            var staff = new Staff { Name = staffDto.Name };
            _staffRepository.Add(staff);
        }

        public void UpdateStaff(int id, StaffRequestDTO staffDto)
        {
            var staff = _staffRepository.GetById(id);
            if (staff == null) throw new KeyNotFoundException("Staff not found");
            staff.Name = staffDto.Name;
            _staffRepository.Update(staff);
        }

        public void DeleteStaff(int id)
        {
            _staffRepository.Delete(id);
        }

        public StaffResponseDTO? GetStaffById(int id)
        {
            var staff = _staffRepository.GetById(id);
            if (staff == null) return null;

            return new StaffResponseDTO
            {
                Id = staff.Id,
                Name = staff.Name,
                Rooms = staff.Rooms.Select(r => new RoomResponseDTO
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    StaffId = r.StaffId,
                    StaffName = r.Staff?.Name,
                    Students = r.Students.Select(s => new StudentResponseDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        RoomId = s.RoomId,
                        RoomNumber = s.Room?.RoomNumber
                    }).ToList()
                }).ToList()
            };
        }

        public System.Collections.Generic.List<StaffResponseDTO> GetAllStaff()
        {
            return _staffRepository.GetAll().Select(st => new StaffResponseDTO
            {
                Id = st.Id,
                Name = st.Name,
                Rooms = st.Rooms.Select(r => new RoomResponseDTO
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    StaffId = r.StaffId,
                    StaffName = r.Staff?.Name,
                    Students = r.Students.Select(s => new StudentResponseDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        RoomId = s.RoomId,
                        RoomNumber = s.Room?.RoomNumber
                    }).ToList()
                }).ToList()
            }).ToList();
        }
    }
}
