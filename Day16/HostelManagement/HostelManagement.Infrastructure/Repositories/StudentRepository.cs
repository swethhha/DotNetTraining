using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;

namespace HostelManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();
        private int _nextId = 1;

        public void Add(Student entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Id = _nextId++;
            _students.Add(entity);
        }

        public void Update(Student entity)
        {
            var existing = _students.FirstOrDefault(s => s.Id == entity.Id);
            if (existing != null)
            {
                existing.Name = entity.Name;
                existing.Department = entity.Department;
                existing.RoomId = entity.RoomId;
                existing.StaffId = entity.StaffId;
            }
        }

        public void Delete(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student != null)
                _students.Remove(student);
        }

        public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

        public List<Student> GetAll() => _students;
    }
}
