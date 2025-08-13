using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HostelManagement.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IRoomRepository _roomRepository;

        public StaffService(IStaffRepository staffRepository, IRoomRepository roomRepository)
        {
            _staffRepository = staffRepository;
            _roomRepository = roomRepository;
        }

        public void AddStaff(StaffRequestDTO staffDto)
        {
            var newStaff = new Staff
            {
                Name = staffDto.Name,
                Capacity = 5
            };
            _staffRepository.Add(newStaff);
        }

        public void UpdateStaff(int id, StaffRequestDTO staffDto)
        {
            var existingStaff = _staffRepository.GetById(id);
            if (existingStaff != null)
            {
                existingStaff.Name = staffDto.Name;
                _staffRepository.Update(existingStaff);
            }
        }

        public void DeleteStaff(int id) => _staffRepository.Delete(id);

        public StaffResponseDTO? GetStaffById(int id)
        {
            var staff = _staffRepository.GetById(id);
            if (staff == null) return null;

            var rooms = staff.Students
                .Select(st => st.Room)
                .Where(r => r != null)
                .ToList();

            return new StaffResponseDTO
            {
                Id = staff.Id,
                Name = staff.Name,
                Capacity = staff.Capacity,
                RoomsManaged = rooms.Select(r => r!.Id).Distinct().Count(),
                RoomNumbers = rooms.Select(r => r!.RoomNumber ?? string.Empty).ToList()
            };
        }

        public List<StaffResponseDTO> GetAllStaff()
        {
            return _staffRepository.GetAll().Select(staff =>
            {
                var rooms = staff.Students
                    .Select(st => st.Room)
                    .Where(r => r != null)
                    .ToList();

                return new StaffResponseDTO
                {
                    Id = staff.Id,
                    Name = staff.Name,
                    Capacity = staff.Capacity,
                    RoomsManaged = rooms.Select(r => r!.Id).Distinct().Count(),
                    RoomNumbers = rooms.Select(r => r!.RoomNumber ?? string.Empty).ToList()
                };
            }).ToList();
        }
    }
}
