using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;

namespace HostelManagement.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomResponseDTO>> GetAllRoomsAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return rooms.Select(r => new RoomResponseDTO
            {
                Id = r.Id,                 // corrected from RoomId
                RoomNumber = r.RoomNumber,
                Capacity = r.Capacity,
                Students = r.Students.Select(s => s.Name).ToList() // assuming Room has navigation property Students
            }).ToList();
        }

        public async Task<RoomResponseDTO?> GetRoomByIdAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return null;

            return new RoomResponseDTO
            {
                Id = room.Id,               // corrected
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                Students = room.Students.Select(s => s.Name).ToList()
            };
        }

        public async Task AddRoomAsync(RoomRequestDTO roomRequest)
        {
            var room = new Room
            {
                RoomNumber = roomRequest.RoomNumber,
                Capacity = roomRequest.Capacity
            };

            await _roomRepository.AddAsync(room);
        }

        public async Task UpdateRoomAsync(int id, RoomRequestDTO roomRequest)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room != null)
            {
                room.RoomNumber = roomRequest.RoomNumber;
                room.Capacity = roomRequest.Capacity;

                await _roomRepository.UpdateAsync(room);
            }
        }

        public async Task DeleteRoomAsync(int id)
        {
            await _roomRepository.DeleteAsync(id);
        }
    }
}
