using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeTracker.Core.Interfaces;
using EmployeeTracker.Core.Entities;

namespace EmployeeTracker.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
       private readonly List<Employee> _employees = new List<Employee>();
        private int _nextId = 1;
        public void Add(Employee entity)
        {
            entity.EmployeeId = _nextId++;
            _employees.Add(entity); ;
        }
        public void Update(Employee entity)
        {
            var existingEmployee = GetById(entity.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.EmployeeName = entity.EmployeeName;
                existingEmployee.Email = entity.Email;
                existingEmployee.Designation = entity.Designation;
                existingEmployee.Department = entity.Department;
            }
        }
        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }
        public List<Employee> GetAll()
        {
            return _employees;
        }
        public Employee? GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.EmployeeId == id);
        }
    }
}
