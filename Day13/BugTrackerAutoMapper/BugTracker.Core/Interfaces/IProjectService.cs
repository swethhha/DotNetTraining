using System.Collections.Generic;
using BugTracker.Core.DTOs;

namespace BugTracker.Core.Interfaces
{
    public interface IProjectService
    {
        void AddProject(ProjectRequestDTO projectRequest);
        void UpdateProject(ProjectRequestDTO projectRequest);
        void DeleteProject(int id);
        ProjectResponseDTO GetProjectById(int id);
        List<ProjectResponseDTO> GetAllProjects();
    }
}
