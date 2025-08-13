using BugTrack.Core.Entities;
using BugTrack.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrack.Infrastructure.Data;
namespace BugTrack.Infrastructure.Repositories
{
    public class BugRepository : IBugRepository
    {
        private readonly List<Bug> _bugs = new();
        private readonly BugTrackerContext _context;
        public BugRepository(BugTrackerContext context)
        {
            _context = context;
        }
        private int _nextId = 1;

        public List<Bug> GetAll()
        {
            return _bugs;
        }

        public Bug? GetById(int id)
        {
            return _bugs.FirstOrDefault(b => b.Id == id);
        }

        public void Add(Bug entity)
        {
            entity.Id = _nextId++;
            entity.CreatedOn = DateTime.Now;
            _bugs.Add(entity);
        }

        public void Update(Bug entity)
        {
            var existing = _bugs.FirstOrDefault(b => b.Id == entity.Id);
            if (existing != null)
            {
                existing.Title = entity.Title;
                existing.Description = entity.Description;
                existing.Status = entity.Status;
                existing.ProjectId = entity.ProjectId;
            }
        }

        public void Delete(int id)
        {
            var bug = _bugs.FirstOrDefault(b => b.Id == id);
            if (bug != null)
            {
                _bugs.Remove(bug);
            }
        }

        public void SaveChanges()
        {
            // No-op for in-memory list
        }

        public async Task<IEnumerable<Bug>> GetAllAsync()
        {
            return await Task.FromResult(_bugs);
        }
        public async Task<Bug?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_bugs.FirstOrDefault(b => b.Id == id));
        }
        public async Task AddAsync(Bug bug)
        {

            await _context.Bugs.AddAsync(bug);
        }
        public async Task UpdateAsync(Bug bug)
        {
            _context.Bugs.Update(bug);
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            if (bug != null)
            {
                _context.Bugs.Remove(bug);
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}