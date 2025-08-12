using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using System;
using System.Linq;

namespace HostelManagement.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IStaffRepository _staffRepository;

        public RoomService(IRoomRepository roomRepository, IStaffRepository staffRepository)
        {
            _roomRepository = roomRepository;
            _staffRepository = staffRepository;
        }

        public void AddRoom(RoomRequestDTO roomDto)
        {
            // if staff provided, ensure staff exists and has < 5 rooms
            if (roomDto.StaffId.HasValue)
            {
                var st = _staffRepository.GetById(roomDto.StaffId.Value) ?? throw new KeyNotFoundException("Staff not found");
                if (st.Rooms.Count >= 5) throw new InvalidOperationException("Staff already manages 5 rooms");
            }

            var room = new Room
            {
                RoomNumber = roomDto.RoomNumber,
                StaffId = roomDto.StaffId
            };

            _roomRepository.Add(room);

            // if staff assigned, update staff's Rooms collection
            if (roomDto.StaffId.HasValue)
            {
                var staff = _staffRepository.GetById(roomDto.StaffId.Value);
                if (staff != null) staff.Rooms.Add(room);
            }
        }

        public void UpdateRoom(int id, RoomRequestDTO roomDto)
        {
            var room = _roomRepository.GetById(id) ?? throw new KeyNotFoundException("Room not found");

            // if changing staff, verify limit
            if (roomDto.StaffId.HasValue && roomDto.StaffId != room.StaffId)
            {
                var newStaff = _staffRepository.GetById(roomDto.StaffId.Value) ?? throw new KeyNotFoundException("Staff not found");
                if (newStaff.Rooms.Count >= 5) throw new InvalidOperationException("Staff already manages 5 rooms");

                // unassign from old staff
                if (room.StaffId.HasValue)
                {
                    var old = _staffRepository.GetById(room.StaffId.Value);
                    if (old != null)
                    {
                        var r = old.Rooms.FirstOrDefault(x => x.Id == room.Id);
                        if (r != null) old.Rooms.Remove(r);
                    }
                }

                room.StaffId = roomDto.StaffId;
                newStaff.Rooms.Add(room);
            }

            room.RoomNumber = roomDto.RoomNumber;
            _roomRepository.Update(room);
        }

        public void DeleteRoom(int id)
        {
            var r = _roomRepository.GetById(id) ?? throw new KeyNotFoundException("Room not found");

            // unassign students
            foreach (var s in r.Students.ToList())
            {
                s.RoomId = null;
                s.Room = null;
            }

            // unassign staff
            if (r.StaffId.HasValue)
            {
                var st = _staffRepository.GetById(r.StaffId.Value);
                if (st != null)
                {
                    var rr = st.Rooms.FirstOrDefault(x => x.Id == r.Id);
                    if (rr != null) st.Rooms.Remove(rr);
                }
            }

            _roomRepository.Delete(id);
        }

        public RoomResponseDTO? GetRoomById(int id)
        {
            var r = _roomRepository.GetById(id);
            if (r == null) return null;

            return new RoomResponseDTO
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                // Capacity is fixed in DTO as read-only
                StaffId = r.StaffId,
                StaffName = r.Staff?.Name,
                Students = r.Students.Select(s => new StudentResponseDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    RoomId = s.RoomId,
                    RoomNumber = s.Room?.RoomNumber
                }).ToList()
            };
        }

        public System.Collections.Generic.List<RoomResponseDTO> GetAllRooms()
        {
            return _roomRepository.GetAll().Select(r => new RoomResponseDTO
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
            }).ToList();
        }
    }
}

