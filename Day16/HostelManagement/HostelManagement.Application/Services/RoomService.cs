using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HostelManagement.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public void AddRoom(RoomRequestDTO roomDto)
        {
            var newRoom = new Room
            {
                RoomNumber = roomDto.RoomNumber,
                Capacity = roomDto.Capacity
            };
            _roomRepository.Add(newRoom);
        }

        public void UpdateRoom(int id, RoomRequestDTO roomDto)
        {
            var room = _roomRepository.GetById(id);
            if (room != null)
            {
                room.RoomNumber = roomDto.RoomNumber;
                room.Capacity = roomDto.Capacity;
                _roomRepository.Update(room);
            }
        }

        public void DeleteRoom(int id) => _roomRepository.Delete(id);

        public RoomResponseDTO? GetRoomById(int id)
        {
            var room = _roomRepository.GetById(id);
            if (room == null) return null;

            return new RoomResponseDTO
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                Students = room.Students.Select(s => s.Name).ToList()
                // CurrentOccupancy automatically derived
            };
        }

        public List<RoomResponseDTO> GetAllRooms()
        {
            return _roomRepository.GetAll().Select(room => new RoomResponseDTO
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                Students = room.Students.Select(s => s.Name).ToList()
                // CurrentOccupancy automatically derived
            }).ToList();
        }
    }
}
