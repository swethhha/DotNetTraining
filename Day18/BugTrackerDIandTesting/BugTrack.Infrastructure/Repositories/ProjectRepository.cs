using BugTrack.Core.Entities;
using BugTrack.Core.Interfaces;
using BugTrack.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrack.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly List<Project> _projects = new();
        private readonly BugTrackerContext _context;
        private int _nextId = 1;

        public ProjectRepository(BugTrackerContext context)
        {
            _context = context;
        }

        // Sync
        public List<Project> GetAll() => _projects;

        public Project? GetById(int id) =>
            _projects.FirstOrDefault(p => p.ProjectId == id);

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
            // No-op for in-memory
        }

        // Async
        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await Task.FromResult(_projects);
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_projects.FirstOrDefault(p => p.ProjectId == id));
        }

        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
