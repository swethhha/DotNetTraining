using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeTracker.Core.Entities;
using EmployeeTracker.Core.Interfaces;


namespace EmployeeTracker.Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo) => _repo = repo;

        public void Add(Employee e) => _repo.Add(e);
        public void Delete(int id) => _repo.Delete(id);
        public void Update(Employee e) => _repo.Update(e);
        public Employee GetById(int id) => _repo.GetById(id);
        public List<Employee> GetAll() => _repo.GetAll();
    }
}
