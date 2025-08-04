using BugDashboardStats.Core.Interfaces;
using BugDashboardStats.Infrastructure.DTOs;

namespace BugDashboardStats.Application.Services
{
    public class BugService
    {
        private readonly IBugRepository _repo;

        public BugService(IBugRepository repo)
        {
            _repo = repo;
        }

        public List<BugDto> GetAllBugs()
        {
            return _repo.GetAll().Select(b => new BugDto
            {
                Title = b.Title,
                Status = b.Status,
                Priority = b.Priority,
                Project = b.Project.Name,
                Creator = b.Creator.Username,
                CreatedDate = b.CreatedDate
            }).ToList();
        }

        public List<BugGroupedStatsDto> GetBugCountByStatus()
        {
            return _repo.GetAll()
                .GroupBy(b => b.Status)
                .Select(g => new BugGroupedStatsDto { Key = g.Key, Count = g.Count() })
                .ToList();
        }

        public List<BugGroupedStatsDto> GetBugCountByPriority()
        {
            return _repo.GetAll()
                .GroupBy(b => b.Priority)
                .Select(g => new BugGroupedStatsDto { Key = g.Key, Count = g.Count() })
                .ToList();
        }

        public List<BugGroupedStatsDto> GetBugCountByProject()
        {
            return _repo.GetAll()
                .GroupBy(b => b.Project.Name)
                .Select(g => new BugGroupedStatsDto { Key = g.Key, Count = g.Count() })
                .ToList();
        }
    }
}
