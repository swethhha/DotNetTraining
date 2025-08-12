using System.Collections.Generic;
using AutoMapper;
using EmployeeTracker.Core.Entities;
using EmployeeTracker.Core.DTOs;
using EmployeeTracker.Core.Interfaces;

namespace EmployeeTracker.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public void AddEmployee(EmployeeRequestDTO employeeRequestDTO)
        {
            var employee = _mapper.Map<Employee>(employeeRequestDTO);
            _employeeRepository.Add(employee);
        }

        public void UpdateEmployee(EmployeeRequestDTO employeeRequestDTO)
        {
            var existing = _employeeRepository.GetById(employeeRequestDTO.EmployeeId);
            if (existing != null)
            {
                // Manual property assignment OR use _mapper.Map(dto, existing)
                existing.EmployeeName = employeeRequestDTO.EmployeeName;
                existing.Email = employeeRequestDTO.Email;
                existing.Designation = employeeRequestDTO.Designation;
                existing.Department = employeeRequestDTO.Department;

                _employeeRepository.Update(existing);
            }
        }

        public EmployeeRequestDTO? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee != null ? _mapper.Map<EmployeeRequestDTO>(employee) : null;
        }

        public List<EmployeeRequestDTO> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAll();
            return _mapper.Map<List<EmployeeRequestDTO>>(employees);
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepository.Delete(id);
        }
    }
}
