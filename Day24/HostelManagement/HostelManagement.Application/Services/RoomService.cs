using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Exceptions;
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
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                Capacity = r.Capacity,
                Students = r.Students.Select(s => s.Name).ToList()
            }).ToList();
        }

        public async Task<RoomResponseDTO?> GetRoomByIdAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                throw new NotFoundException($"Room with ID {id} not found.");

            return new RoomResponseDTO
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                Students = room.Students.Select(s => s.Name).ToList()
            };
        }

        public async Task AddRoomAsync(RoomRequestDTO roomRequest)
        {
            if (string.IsNullOrWhiteSpace(roomRequest.RoomNumber))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "RoomNumber", new[] { "Room number is required." } }
                });
            }

            if (roomRequest.Capacity <= 0)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Capacity", new[] { "Capacity must be greater than zero." } }
                });
            }

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
            if (room == null)
                throw new NotFoundException($"Room with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(roomRequest.RoomNumber))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "RoomNumber", new[] { "Room number is required." } }
                });
            }

            room.RoomNumber = roomRequest.RoomNumber;
            room.Capacity = roomRequest.Capacity;

            await _roomRepository.UpdateAsync(room);
        }

        public async Task DeleteRoomAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
                throw new NotFoundException($"Room with ID {id} not found.");

            await _roomRepository.DeleteAsync(id);
        }
    }
}
