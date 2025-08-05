using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using EmployeeTracker.Core.Entities;

namespace EmployeeTracker.Core.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
    }
}
