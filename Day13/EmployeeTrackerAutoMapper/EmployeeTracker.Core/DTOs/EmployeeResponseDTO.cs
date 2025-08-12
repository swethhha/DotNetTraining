using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTracker.Core.DTOs
{
    public class EmployeeResponseDTO
    {
        public string? EmployeeName { get; set; }
        public string ? Designation { get; set; }
        public string ? Department { get; set; }
    }
}
