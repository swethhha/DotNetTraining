using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTracker.Core.DTOs
{
    public class EmployeeRequestDTO
    {
        public string? EmployeeName { get; set; }
        public int EmployeeId { get; set; } 
        public string? Email { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
    }
}
