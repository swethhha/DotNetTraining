using System;
using System.Collections.Generic;
using System.Linq;
using BugTracker.Core.Entities;
using BugTracker.Core.Interfaces;

namespace BugTracker.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly List<Project> _projects = new();
        private int _nextId = 1;

        public void Add(Project project)
        {
            project.Id = _nextId++;
            _projects.Add(project);
        }

        public void Update(Project updatedProject)
        {
            var existing = GetById(updatedProject.Id);
            if (existing != null)
            {
                existing.Title = updatedProject.Title;
            }
        }

        public void Delete(int id)
        {
            var project = GetById(id);
            if (project != null)
            {
                _projects.Remove(project);
            }
        }

        public Project GetById(int id)
        {
            return _projects.FirstOrDefault(p => p.Id == id);
        }

        public List<Project> GetAll()
        {
            return _projects;
        }
    }
}
