using System.Collections.Generic;
using IssueTracker.Core.Entities;
using IssueTracker.Core.Interfaces;
using IssueTracker.Application.Interfaces;

namespace IssueTracker.Application.Services
{
    public class BugService : IBugService
    {
        private readonly IBugRepository _repository;

        public BugService(IBugRepository repository)
        {
            _repository = repository;
        }

        public void Add(Bug bug)
        {
            _repository.AddBug(bug);
        }

        public List<Bug> GetAll()
        {
            return _repository.GetAllBugs();
        }

        public Bug GetById(int id)
        {
            return _repository.GetBugById(id);
        }

        public void Update(Bug bug)
        {
            _repository.UpdateBug(bug);
        }

        public void Delete(int id)
        {
            _repository.DeleteBug(id);
        }
    }
}
