using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HostelManagement.Infrastructure.Services
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
            if (s == null) return null;

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
            var student = new Student
            {
                Name = studentRequest.Name,
                Department = studentRequest.Department,
                RoomId = 0, // leave null/0 if no assignment
                StaffId = 0 // leave null/0 if no assignment
            };

            await _studentRepo.AddAsync(student);
        }

        public async Task UpdateStudentAsync(int id, StudentRequestDTO studentRequest)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null) return;

            student.Name = studentRequest.Name;
            student.Department = studentRequest.Department;

            await _studentRepo.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepo.DeleteAsync(id);
        }
    }
}
