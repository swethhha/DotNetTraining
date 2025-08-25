using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Exceptions;
using HostelManagement.Core.Interfaces;

namespace HostelManagement.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;

        public StudentService(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public async Task<List<StudentResponseDTO>> GetAllStudentsAsync()
        {
            var students = await _studentRepo.GetAllAsync();
            return students.Select(s => new StudentResponseDTO
            {
                Id = s.Id,
                Name = s.Name,
                Department = s.Department,
                RoomNumber = s.Room?.RoomNumber ?? "",
                StaffName = s.Staff?.Name ?? ""
            }).ToList();
        }

        public async Task<StudentResponseDTO?> GetStudentByIdAsync(int id)
        {
            var s = await _studentRepo.GetByIdAsync(id);
            if (s == null)
                throw new NotFoundException($"Student with ID {id} not found.");

            return new StudentResponseDTO
            {
                Id = s.Id,
                Name = s.Name,
                Department = s.Department,
                RoomNumber = s.Room?.RoomNumber ?? "",
                StaffName = s.Staff?.Name ?? ""
            };
        }

        public async Task AddStudentAsync(StudentRequestDTO studentRequest)
        {
            if (string.IsNullOrWhiteSpace(studentRequest.Name))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Name", new[] { "Student name is required." } }
                });
            }

            if (string.IsNullOrWhiteSpace(studentRequest.Department))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Department", new[] { "Department is required." } }
                });
            }

            var student = new Student
            {
                Name = studentRequest.Name,
                Department = studentRequest.Department,
                RoomId = 0,
                StaffId = 0
            };

            await _studentRepo.AddAsync(student);
        }

        public async Task UpdateStudentAsync(int id, StudentRequestDTO studentRequest)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found.");

            student.Name = studentRequest.Name;
            student.Department = studentRequest.Department;

            await _studentRepo.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found.");

            await _studentRepo.DeleteAsync(id);
        }
    }
}
