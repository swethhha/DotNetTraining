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
        void AddStaff(StaffRequestDTO staffDto);
        void UpdateStaff(int id, StaffRequestDTO staffDto);
        void DeleteStaff(int id);
        StaffResponseDTO? GetStaffById(int id);
        
        List<StaffResponseDTO> GetAllStaff();
    }
}
