using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HostelManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();
        private int _nextId = 1;

        public void Add(Student entity)
        {
            entity.Id = _nextId++;
            _students.Add(entity);
        }

        public void Update(Student entity)
        {
            var existing = GetById(entity.Id);
            if (existing == null) return;

            existing.Name = entity.Name;
            existing.RoomId = entity.RoomId;
            existing.Room = entity.Room;
        }

        public void Delete(int id)
        {
            var s = GetById(id);
            if (s != null) _students.Remove(s);
        }

        public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

        public List<Student> GetAll() => _students;
    }
}
