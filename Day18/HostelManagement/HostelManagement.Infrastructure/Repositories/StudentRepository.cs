using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;

namespace HostelManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();

        public Task<List<Student>> GetAllAsync()
        {
            return Task.FromResult(_students.ToList());
        }

        public Task<Student?> GetByIdAsync(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(student);
        }

        public Task AddAsync(Student student)
        {
            _students.Add(student);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Student student)
        {
            var existing = _students.FirstOrDefault(s => s.Id == student.Id);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.Department = student.Department;
                existing.RoomId = student.RoomId;
                existing.StaffId = student.StaffId;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student != null)
                _students.Remove(student);

            return Task.CompletedTask;
        }

        public Task<List<Student>> GetStudentsByRoomIdAsync(int roomId)
        {
            var list = _students.Where(s => s.RoomId == roomId).ToList();
            return Task.FromResult(list);
        }
    }
}
