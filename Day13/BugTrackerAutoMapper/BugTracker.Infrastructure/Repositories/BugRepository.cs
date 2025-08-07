using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Core.DTOs;
using BugTracker.Core.Entities;
using BugTracker.Core.Interfaces;
namespace BugTracker.Infrastructure.Repositories
{
    public class BugRepository : IBugRepository
    {
        private readonly List<Bug> _bugs = new();
        private int _nextId = 1;

        public void Add(Bug bug)
        {
            bug.Id = _nextId++;
            bug.CreatedAt = DateTime.Now;
            _bugs.Add(bug);
        }

        public void Delete(int id)
        {
            var bug = GetById(id);
            if (bug != null) _bugs.Remove(bug);
        }

        public List<Bug> GetAll() => _bugs;

        public Bug GetById(int id) => _bugs.FirstOrDefault(b => b.Id == id);

        public void Update(Bug updatedBug)
        {
            var existing = GetById(updatedBug.Id);
            if (existing != null)
            {
                existing.Title = updatedBug.Title;
                existing.Description = updatedBug.Description;
                existing.Status = updatedBug.Status;
                existing.DueDate = updatedBug.DueDate;
            }
        }
    }
}
