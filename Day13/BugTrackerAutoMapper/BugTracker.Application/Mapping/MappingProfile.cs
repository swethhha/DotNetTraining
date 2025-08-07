using AutoMapper;
using BugTracker.Core.DTOs;
using BugTracker.Core.Entities;

namespace BugTracker.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Bug mappings
            CreateMap<Bug, BugRequestDTO>().ReverseMap();
            CreateMap<Bug, BugResponseDTO>();

            // User mappings
            CreateMap<User, UserRequestDTO>().ReverseMap();
            CreateMap<User, UserResponseDTO>();

            // Project mappings
            CreateMap<Project, ProjectRequestDTO>().ReverseMap();
            CreateMap<Project, ProjectResponseDTO>();
        }
    }
}
