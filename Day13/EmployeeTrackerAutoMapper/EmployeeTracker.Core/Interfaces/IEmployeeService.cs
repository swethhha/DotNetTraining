using EmployeeTracker.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTracker.Core.Interfaces
{
    public interface IEmployeeService
    {
        public void AddEmployee(EmployeeRequestDTO employeeRequestDTO);
        public void UpdateEmployee(EmployeeRequestDTO employeeRequestDTO);
        EmployeeRequestDTO? GetEmployeeById(int id);
        List<EmployeeRequestDTO> GetAllEmployees();
        public void DeleteEmployee(int id);
    }
}
