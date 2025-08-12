using BugTrack.Core.DTOs;
using BugTrack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrack.Core.Interfaces
{
    public interface IProjectService
    {
        void AddProject(ProjectRequestDTO project);
        void UpdateProject(int id, ProjectRequestDTO project);
        void DeleteProject(int id);
        List<Project> GetAllProjects();
        ProjectResponseDTO? GetProjectById(int id);
    }
}
