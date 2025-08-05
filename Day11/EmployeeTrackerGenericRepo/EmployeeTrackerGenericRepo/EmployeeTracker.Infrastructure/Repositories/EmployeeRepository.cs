using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeTracker.Core.Entities;
using EmployeeTracker.Core.Interfaces;


namespace EmployeeTracker.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees = new();

        public void Add(Employee employee) => _employees.Add(employee);
        public void Delete(int id) => _employees.RemoveAll(e => e.Id == id);
        public List<Employee> GetAll() => _employees;
        public Employee GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);
        public void Update(Employee employee)
        {
            var index = _employees.FindIndex(e => e.Id == employee.Id);
            if (index != -1) _employees[index] = employee;
        }
    }
}

