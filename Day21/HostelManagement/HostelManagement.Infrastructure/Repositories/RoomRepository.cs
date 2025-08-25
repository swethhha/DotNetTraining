using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;

namespace HostelManagement.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly List<Room> _rooms = new();

        public Task<List<Room>> GetAllAsync()
        {
            return Task.FromResult(_rooms.ToList());
        }

        public Task<Room?> GetByIdAsync(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(room);
        }

        public Task AddAsync(Room room)
        {
            _rooms.Add(room);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Room room)
        {
            var existing = _rooms.FirstOrDefault(r => r.Id == room.Id);
            if (existing != null)
            {
                existing.RoomNumber = room.RoomNumber;
                existing.Capacity = room.Capacity;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room != null)
                _rooms.Remove(room);

            return Task.CompletedTask;
        }
    }
}
