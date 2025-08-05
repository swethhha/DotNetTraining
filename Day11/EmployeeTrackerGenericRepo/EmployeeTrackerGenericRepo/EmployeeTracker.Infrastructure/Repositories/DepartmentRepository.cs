using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeTracker.Core.Entities;
using EmployeeTracker.Core.Interfaces;


namespace EmployeeTracker.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private List<Department> _departments = new();

        public void Add(Department department) => _departments.Add(department);
        public void Delete(int id) => _departments.RemoveAll(d => d.DeptId == id);
        public List<Department> GetAll() => _departments;
        public Department GetById(int id) => _departments.FirstOrDefault(d => d.DeptId == id);
        public void Update(Department department)
        {
            var index = _departments.FindIndex(d => d.DeptId == department.DeptId);
            if (index != -1) _departments[index] = department;
        }
    }
}

