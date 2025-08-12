using EmployeeTracker.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTracker.Core.Interfaces
{
    public interface IDepartmentService
    {
        public void AddDepartment(DepartmentRequestDTO departmentRequestDTO);
        public void UpdateDepartment(DepartmentRequestDTO departmentRequestDTO);
        DepartmentRequestDTO? GetDepartmentById(int id);
        List<DepartmentRequestDTO> GetAllDepartments();
        public void DeleteDepartment(int id);
    }
}
