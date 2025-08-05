using BugTrackerGenericRepo.Core.Entities;
using BugTrackerGenericRepo.Core.Interfaces;
using System.Collections.Generic;

namespace BugTrackerGenericRepo.Application.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void CreateProject(Project project)
        {
            _projectRepository.Add(project);
        }

        public List<Project> GetAllProjects()
        {
            return _projectRepository.GetAll();
        }

        public void UpdateProject(Project project)
        {
            _projectRepository.Update(project);
        }

        public void DeleteProject(int id)
        {
            _projectRepository.Delete(id);
        }
    }
}
