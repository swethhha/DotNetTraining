using BugTrackerGenericRepo.Core.Entities;
using BugTrackerGenericRepo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerGenericRepo.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly List<Project> _projects = new List<Project>();

        public void Add(Project entity)
        {
            _projects.Add(entity);
        }

        public void Delete(int id)
        {
            var project = _projects.FirstOrDefault(p => p.ProjectId == id);
            if (project != null)
                _projects.Remove(project);
        }

        public List<Project> GetAll()
        {
            return _projects;
        }

        public Project GetById(int id)
        {
            return _projects.FirstOrDefault(p => p.ProjectId == id);
        }

        public void Update(Project entity)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectId == entity.ProjectId);
            if (existing != null)
            {
                existing.ProjectName = entity.ProjectName;
            }
        }
    }
}