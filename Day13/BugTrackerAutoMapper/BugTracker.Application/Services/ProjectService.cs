using AutoMapper;
using BugTracker.Core.DTOs;
using BugTracker.Core.Entities;
using BugTracker.Core.Interfaces;
using System.Collections.Generic;

namespace BugTracker.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public void AddProject(ProjectRequestDTO projectRequest)
        {
            var project = _mapper.Map<Project>(projectRequest);
            _projectRepository.Add(project);
        }

        public void UpdateProject(ProjectRequestDTO projectRequest)
        {
            var project = _mapper.Map<Project>(projectRequest);
            _projectRepository.Update(project);
        }

        public void DeleteProject(int id)
        {
            _projectRepository.Delete(id);
        }

        public ProjectResponseDTO GetProjectById(int id)
        {
            var project = _projectRepository.GetById(id);
            return _mapper.Map<ProjectResponseDTO>(project);
        }

        public List<ProjectResponseDTO> GetAllProjects()
        {
            var projects = _projectRepository.GetAll();
            return _mapper.Map<List<ProjectResponseDTO>>(projects);
        }
    }
}
