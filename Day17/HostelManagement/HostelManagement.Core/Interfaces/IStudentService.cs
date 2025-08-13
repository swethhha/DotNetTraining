using HostelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelManagement.Core.Interfaces
{
    public interface IStudentService
    {
        void AddStudent(StudentRequestDTO studentDto);
        void UpdateStudent(int id, StudentRequestDTO studentDto);
        void DeleteStudent(int id);
        StudentResponseDTO? GetStudentById(int id);
        
        List<StudentResponseDTO> GetAllStudents();
    }
}
