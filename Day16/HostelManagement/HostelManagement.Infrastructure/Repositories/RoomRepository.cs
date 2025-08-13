using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;

namespace HostelManagement.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly List<Room> _rooms = new();
        private int _nextId = 1;

        public void Add(Room entity)
        {
            entity.Id = _nextId++;
            _rooms.Add(entity);
        }

        public void Update(Room entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.RoomNumber = entity.RoomNumber;
                existing.Capacity = entity.Capacity;
                existing.Students = entity.Students;
            }
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing != null)
                _rooms.Remove(existing);
        }

        public Room? GetById(int id) => _rooms.FirstOrDefault(r => r.Id == id);

        public List<Room> GetAll() => _rooms;
    }
}
