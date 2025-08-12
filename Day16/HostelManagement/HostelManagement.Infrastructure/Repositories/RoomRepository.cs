using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HostelManagement.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly List<Room> _rooms = new();
        private int _nextId = 1;

        public void Add(Room entity)
        {
            // assign id first so GenerateRoomNumber() can use it
            entity.Id = _nextId++;
            entity.GenerateRoomNumber();
            _rooms.Add(entity);
        }

        public void Update(Room entity)
        {
            var existing = GetById(entity.Id);
            if (existing == null) return;

            existing.RoomNumber = entity.RoomNumber;
            existing.StaffId = entity.StaffId;
            existing.Staff = entity.Staff;
            existing.Students = entity.Students;
            // Capacity is fixed in entity, no update needed
        }

        public void Delete(int id)
        {
            var room = GetById(id);
            if (room != null) _rooms.Remove(room);
        }

        public Room? GetById(int id) => _rooms.FirstOrDefault(r => r.Id == id);

        public List<Room> GetAll() => _rooms;
    }
}
