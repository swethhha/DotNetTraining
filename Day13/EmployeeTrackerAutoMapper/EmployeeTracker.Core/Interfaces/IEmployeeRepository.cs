using EmployeeTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTracker.Core.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}
