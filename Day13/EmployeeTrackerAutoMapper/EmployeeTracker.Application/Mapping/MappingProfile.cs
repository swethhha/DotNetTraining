using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeTracker.Core.Entities;
using EmployeeTracker.Core.DTOs;
namespace EmployeeTracker.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeRequestDTO>().ReverseMap();
            CreateMap<Employee, EmployeeResponseDTO>();

            CreateMap<Department, DepartmentRequestDTO>().ReverseMap();
            CreateMap<Department, DepartmentResponseDTO>();
        }
    }
}
