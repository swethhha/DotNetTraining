using HostelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelManagement.Core.Interfaces
{
    public interface IStaffService
    {
        Task<List<StaffResponseDTO>> GetAllStaffAsync();
        Task<StaffResponseDTO?> GetStaffByIdAsync(int id);
            
        Task AddStaffAsync(StaffRequestDTO staffRequest);
        
        Task UpdateStaffAsync(int id, StaffRequestDTO staffRequest);
        Task DeleteStaffAsync(int id);
    }
}
