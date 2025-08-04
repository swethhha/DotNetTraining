using BugDashboardStats.Core.Entities;
using BugDashboardStats.Core.Interfaces;

namespace BugDashboardStats.Infrastructure.Repositories
{
    public class BugRepository : IBugRepository
    {
        private readonly List<Bug> _bugs;
        private readonly List<Project> _projects;
        private readonly List<User> _users;

        public BugRepository()
        {
            // Seed users
            _users = new List<User>
            {
                new User { Id = 1, Username = "Alice" },
                new User { Id = 2, Username = "Bob" }
            };

            // Seed projects
            _projects = new List<Project>
            {
                new Project { Id = 1, Name = "Project A" },
                new Project { Id = 2, Name = "Project B" }
            };

            // Seed bugs
            _bugs = new List<Bug>
            {
                new Bug { Id = 1, Title = "Crash on login", Status = "Open", Priority = "High", CreatedDate = DateTime.Now.AddDays(-2), Project = _projects[0], Creator = _users[0] },
                new Bug { Id = 2, Title = "UI glitch", Status = "Resolved", Priority = "Low", CreatedDate = DateTime.Now.AddDays(-1), Project = _projects[0], Creator = _users[1] },
                new Bug { Id = 3, Title = "Performance issue", Status = "Open", Priority = "Medium", CreatedDate = DateTime.Now, Project = _projects[1], Creator = _users[0] },
                new Bug { Id = 4, Title = "Data sync error", Status = "In Progress", Priority = "High", CreatedDate = DateTime.Now.AddDays(-3), Project = _projects[1], Creator = _users[1] }
            };
        }

        public List<Bug> GetAll() => _bugs;

        public List<Bug> GetBugsByProject(string projectName)
        {
            return _bugs.Where(b => b.Project.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Bug> GetBugsByPriority(string priority)
        {
            return _bugs.Where(b => b.Priority.Equals(priority, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Bug> GetBugsByStatus(string status)
        {
            return _bugs.Where(b => b.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
