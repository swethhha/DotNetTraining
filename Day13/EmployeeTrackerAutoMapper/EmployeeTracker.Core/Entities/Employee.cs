using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTracker.Core.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string ? EmployeeName { get; set; }
        public string ? Email { get; set; }
        public string ? Designation { get; set; }
        public string? Department { get; set; }
    }
}
