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
       Task<List<StudentResponseDTO>> GetAllStudentsAsync();
        Task<StudentResponseDTO?> GetStudentByIdAsync(int id);
        Task AddStudentAsync(StudentRequestDTO studentRequest);
        Task UpdateStudentAsync(int id, StudentRequestDTO studentRequest);
        Task DeleteStudentAsync(int id);
    }
}
