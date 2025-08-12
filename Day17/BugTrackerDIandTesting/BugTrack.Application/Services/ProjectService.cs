using BugTrack.Core.DTOs;
using BugTrack.Core.Entities;
using BugTrack.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrack.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public List<Project> GetAllProjects()
        {
            return _projectRepository.GetAll();
        }
        public void AddProject(ProjectRequestDTO project)
        {
            var newProject = new Project
            {
                ProjectName = project.ProjectName,
                Description = project.Description
            };
            _projectRepository.Add(newProject);
            _projectRepository.SaveChanges();
        }
        public void UpdateProject(int id, ProjectRequestDTO project)
        {
            var existingProject = _projectRepository.GetById(id);
            if (existingProject != null)
            {
                existingProject.ProjectName = project.ProjectName;
                existingProject.Description = project.Description;
                _projectRepository.Update(existingProject);
                _projectRepository.SaveChanges();
            }
        }
        public void DeleteProject(int id)
        {
            _projectRepository.Delete(id);
            _projectRepository.SaveChanges();
        }
        public ProjectResponseDTO? GetProjectById(int id)
        {
            var project = _projectRepository.GetById(id);
            if (project == null) return null;
            return new ProjectResponseDTO
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                Description = project.Description
            };
        }
    }
}
