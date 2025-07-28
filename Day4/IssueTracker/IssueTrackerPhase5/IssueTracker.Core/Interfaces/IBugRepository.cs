using System.Collections.Generic;
using IssueTracker.Core.Entities;

namespace IssueTracker.Core.Interfaces
{
    public interface IBugRepository
    {
        void AddBug(Bug bug);
        List<Bug> GetAllBugs();
        Bug GetBugById(int id);
        void UpdateBug(Bug bug);
        void DeleteBug(int id);
    }
}
