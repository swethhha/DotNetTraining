using BugDashboardStats.Core.Entities;

namespace BugDashboardStats.Core.Interfaces
{
    public interface IBugRepository
    {
        List<Bug> GetAll();
        List<Bug> GetBugsByProject(string projectName);
        List<Bug> GetBugsByPriority(string priority);
        List<Bug> GetBugsByStatus(string status);
    }
}
