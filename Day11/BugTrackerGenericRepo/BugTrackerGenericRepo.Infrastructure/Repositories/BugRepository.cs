using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTrackerGenericRepo.Core.Entities;
using BugTrackerGenericRepo.Core.Interfaces;
using static BugTrackerGenericRepo.Core.Interfaces.IRepository;
namespace BugTrackerGenericRepo.Infrastructure.Repositories
{
    public class BugRepository : IBugRepository
    {
        private readonly List<Bug> _bugs;
        public BugRepository()
        {
            _bugs = new List<Bug>();
        }
        public void Add(Bug entity)
        {
            _bugs.Add(entity);
        }
        public void Update(Bug entity)
        {
            var existingBug = GetById(entity.Id);
            if (existingBug != null)
            {
                existingBug.Title = entity.Title;
                existingBug.Description = entity.Description;
                existingBug.Status = entity.Status;
                existingBug.Priority = entity.Priority;
            }
        }
        public void Delete(int id)
        {
            var bug = GetById(id);
            if (bug != null)
            {
                _bugs.Remove(bug);
            }
        }
        public Bug GetById(int id)
        {
            return _bugs.FirstOrDefault(b => b.Id == id);
        }
        public List<Bug> GetAll()
        {
            return _bugs;
        }
    }
}
