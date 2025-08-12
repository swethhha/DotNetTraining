using BugTrack.Core.Entities;
using BugTrack.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrack.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    
        {
        private readonly List<Project> _projects = new();
        private int _nextId = 1;
        public List<Project> GetAll()
        {
            return _projects;
        }
        public Project? GetById(int id)
        {
            return _projects.FirstOrDefault(p => p.ProjectId == id);
        }
        public void Add(Project entity)
        {
            entity.ProjectId = _nextId++;

            _projects.Add(entity);
        }
        public void Update(Project entity)
        {
            var existing = _projects.FirstOrDefault(p => p.ProjectId == entity.ProjectId);
            if (existing != null)
            {
                existing.ProjectName = entity.ProjectName;
                existing.Description = entity.Description;

            }
        }
        public void Delete(int id)
        {
            var project = _projects.FirstOrDefault(p => p.ProjectId == id);
            if (project != null)
            {
                _projects.Remove(project);
            }
        }
        public void SaveChanges()
        {
            // No-op for in-memory list
        }
    }
}
