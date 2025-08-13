using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HostelManagement.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IStaffRepository _staffRepository;

        public StudentService(IStudentRepository studentRepo, IRoomRepository roomRepo, IStaffRepository staffRepo)
        {
            _studentRepository = studentRepo;
            _roomRepository = roomRepo;
            _staffRepository = staffRepo;
        }

        public void AddStudent(StudentRequestDTO studentDto)
        {
            var availableRoom = _roomRepository.GetAll()
                .FirstOrDefault(r => r.Students.Count < r.Capacity);

            if (availableRoom == null)
                throw new Exception("No available rooms. Please create a new room first.");

            var availableStaff = _staffRepository.GetAll()
                .FirstOrDefault(s => s.Students.Select(st => st.RoomId).Distinct().Count() < s.Capacity);

            if (availableStaff == null)
                throw new Exception("No available staff. Please add a new staff member.");

            var newStudent = new Student
            {
                Name = studentDto.Name,
                Department = studentDto.Department,
                RoomId = availableRoom.Id,
                StaffId = availableStaff.Id,
                Room = availableRoom,      // Link navigation property
                Staff = availableStaff     // Link navigation property
            };

            availableRoom.Students.Add(newStudent);
            availableStaff.Students.Add(newStudent);

            _studentRepository.Add(newStudent);
        }

        public void UpdateStudent(int id, StudentRequestDTO studentDto)
        {
            var student = _studentRepository.GetById(id);
            if (student != null)
            {
                student.Name = studentDto.Name;
                student.Department = studentDto.Department;
                _studentRepository.Update(student);
            }
        }

        public void DeleteStudent(int id) => _studentRepository.Delete(id);

        public StudentResponseDTO? GetStudentById(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null) return null;

            // Ensure Room and Staff are linked if missing
            if (student.Room == null)
                student.Room = _roomRepository.GetAll().FirstOrDefault(r => r.Id == student.RoomId);

            if (student.Staff == null)
                student.Staff = _staffRepository.GetAll().FirstOrDefault(s => s.Id == student.StaffId);

            return new StudentResponseDTO
            {
                Id = student.Id,
                Name = student.Name,
                Department = student.Department,
                RoomNumber = student.Room?.RoomNumber,
                StaffName = student.Staff?.Name
            };
        }

        public List<StudentResponseDTO> GetAllStudents()
        {
            var rooms = _roomRepository.GetAll();
            var staff = _staffRepository.GetAll();

            return _studentRepository.GetAll().Select(s =>
            {
                var room = rooms.FirstOrDefault(r => r.Id == s.RoomId);
                var staffMember = staff.FirstOrDefault(st => st.Id == s.StaffId);

                return new StudentResponseDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Department = s.Department,
                    RoomNumber = room?.RoomNumber,
                    StaffName = staffMember?.Name
                };
            }).ToList();
        }
    }
}
