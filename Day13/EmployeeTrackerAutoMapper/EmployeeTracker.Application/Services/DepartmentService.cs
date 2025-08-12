using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeTracker.Core.Entities;
using EmployeeTracker.Core.DTOs;
using EmployeeTracker.Core.Interfaces;

namespace EmployeeTracker.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }
        public void AddDepartment(DepartmentRequestDTO departmentRequestDTO)
        {
            var department = _mapper.Map<Department>(departmentRequestDTO);
            _departmentRepository.Add(department);
        }
        public void UpdateDepartment(DepartmentRequestDTO departmentRequestDTO)
        {
            var department = _mapper.Map<Department>(departmentRequestDTO);
            _departmentRepository.Update(department);
        }
        public DepartmentRequestDTO? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            return department != null ? _mapper.Map<DepartmentRequestDTO>(department) : null;
        }
        public List<DepartmentRequestDTO> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            return _mapper.Map<List<DepartmentRequestDTO>>(departments);
        }
        public void DeleteDepartment(int id)
        {
            _departmentRepository.Delete(id);
        }
    }
}
