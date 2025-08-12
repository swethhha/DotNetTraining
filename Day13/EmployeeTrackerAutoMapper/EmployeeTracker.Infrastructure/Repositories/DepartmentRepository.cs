using EmployeeTracker.Core.Entities;
using EmployeeTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTracker.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public List<Department> _departments = new List<Department>();
        public int _nextId = 1;
        public void Add(Department entity)
        {
            entity.DepartmentId = _nextId++;
            _departments.Add(entity);
        }
        public void Update(Department entity)
        {
        
            var existingDepartment = GetById(entity.DepartmentId);
            if (existingDepartment != null)
            {
                existingDepartment.DepartmentName = entity.DepartmentName;
            }

        }
        public void Delete(int id)
        {
            var department = GetById(id);
            if (department != null)
            {
                _departments.Remove(department);
            }
        }
        public List<Department> GetAll()
        {
            return _departments;
        }
        public Department? GetById(int id)
        {
            return _departments.FirstOrDefault(d => d.DepartmentId == id);
        }
    }
}