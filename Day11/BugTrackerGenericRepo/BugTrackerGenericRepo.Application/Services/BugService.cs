using BugTrackerGenericRepo.Core.Entities;
using BugTrackerGenericRepo.Core.Interfaces;
using System.Collections.Generic;

namespace BugTrackerGenericRepo.Application.Services
{
    public class BugService
    {
        private readonly IBugRepository _bugRepository;

        public BugService(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        public void CreateBug(Bug bug)
        {
            bug.CreatedAt = DateTime.Now;
            _bugRepository.Add(bug);
        }

        public List<Bug> GetAllBugs()
        {
            return _bugRepository.GetAll();
        }

        public void UpdateBug(Bug bug)
        {
            _bugRepository.Update(bug);
        }

        public void DeleteBug(int id)
        {
            _bugRepository.Delete(id);
        }
    }
}
