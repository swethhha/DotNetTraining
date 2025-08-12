using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using HostelManagement.Core.Interfaces;
using System;
using System.Linq;

namespace HostelManagement.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IRoomRepository _roomRepository;

        public StudentService(IStudentRepository studentRepository, IRoomRepository roomRepository)
        {
            _studentRepository = studentRepository;
            _roomRepository = roomRepository;
        }

        public void AddStudent(StudentRequestDTO studentDto)
        {
            if (string.IsNullOrWhiteSpace(studentDto.Name))
                throw new ArgumentException("Name is required", nameof(studentDto.Name));

            Room? assignedRoom = null;

            // 1) try preferred room (if provided)
            if (studentDto.PreferredRoomId.HasValue)
            {
                var pref = _roomRepository.GetById(studentDto.PreferredRoomId.Value);
                if (pref != null && pref.Students.Count < pref.Capacity)
                {
                    assignedRoom = pref;
                }
                else
                {
                    // preferred room either doesn't exist or is full -> ignore preferred and auto-assign
                    assignedRoom = null;
                }
            }

            // 2) find first room with space
            if (assignedRoom == null)
            {
                assignedRoom = _roomRepository.GetAll().FirstOrDefault(r => r.Students.Count < r.Capacity);
            }

            // 3) if still none, create a new room
            if (assignedRoom == null)
            {
                var nextNumber = _roomRepository.GetAll().Count + 1;
                var newRoom = new Room
                {
                    RoomNumber = $"R{nextNumber:000}"
                    // Capacity is fixed to 4 in entity
                };
                _roomRepository.Add(newRoom);
                assignedRoom = newRoom;
            }

            // 4) create student and set nav props
            var student = new Student
            {
                Name = studentDto.Name,
                RoomId = assignedRoom.Id,
                Room = assignedRoom
            };

            _studentRepository.Add(student);

            // add to room's Students collection
            assignedRoom.Students.Add(student);
        }

        public void UpdateStudent(int id, StudentRequestDTO studentDto)
        {
            var existing = _studentRepository.GetById(id) ?? throw new KeyNotFoundException("Student not found");
            existing.Name = studentDto.Name;

            // if preferred room specified and different, attempt move
            if (studentDto.PreferredRoomId.HasValue && studentDto.PreferredRoomId.Value != existing.RoomId)
            {
                var newRoom = _roomRepository.GetById(studentDto.PreferredRoomId.Value) ?? throw new KeyNotFoundException("Room not found");
                if (newRoom.Students.Count >= newRoom.Capacity) throw new InvalidOperationException("Room is full");

                // remove from old room collection
                if (existing.RoomId.HasValue)
                {
                    var oldRoom = _roomRepository.GetById(existing.RoomId.Value);
                    if (oldRoom != null)
                    {
                        var s = oldRoom.Students.FirstOrDefault(x => x.Id == existing.Id);
                        if (s != null) oldRoom.Students.Remove(s);
                    }
                }

                // assign new room
                existing.RoomId = newRoom.Id;
                existing.Room = newRoom;
                newRoom.Students.Add(existing);
            }

            _studentRepository.Update(existing);
        }

        public void DeleteStudent(int id)
        {
            var existing = _studentRepository.GetById(id) ?? throw new KeyNotFoundException("Student not found");

            if (existing.RoomId.HasValue)
            {
                var room = _roomRepository.GetById(existing.RoomId.Value);
                if (room != null)
                {
                    var s = room.Students.FirstOrDefault(x => x.Id == existing.Id);
                    if (s != null) room.Students.Remove(s);
                }
            }

            _studentRepository.Delete(id);
        }

        public StudentResponseDTO? GetStudentById(int id)
        {
            var s = _studentRepository.GetById(id);
            if (s == null) return null;
            return new StudentResponseDTO
            {
                Id = s.Id,
                Name = s.Name,
                RoomId = s.RoomId,
                RoomNumber = s.Room?.RoomNumber
            };
        }

        public System.Collections.Generic.List<StudentResponseDTO> GetAllStudents()
        {
            return _studentRepository.GetAll().Select(s => new StudentResponseDTO
            {
                Id = s.Id,
                Name = s.Name,
                RoomId = s.RoomId,
                RoomNumber = s.Room?.RoomNumber
            }).ToList();
        }
    }
}
