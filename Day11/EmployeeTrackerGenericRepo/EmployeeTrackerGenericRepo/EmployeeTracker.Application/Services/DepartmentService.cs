using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeTracker.Core.Entities;
using EmployeeTracker.Core.Interfaces;


namespace EmployeeTracker.Application.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentService(IDepartmentRepository repo) => _repo = repo;

        public void Add(Department d) => _repo.Add(d);
        public void Delete(int id) => _repo.Delete(id);
        public void Update(Department d) => _repo.Update(d);
        public Department GetById(int id) => _repo.GetById(id);
        public List<Department> GetAll() => _repo.GetAll();
    }
}

