using BugStatisticsDashboard.Core.Entities;
using BugStatisticsDashboard.Core.Interface;
using System;
using System.Collections.Generic;

namespace BugStatisticsDashboard.Infrastructure.Repositories
{
    public class BugRepository : IBugRepository
    {
        private readonly List<Bug> _bugs;

        public BugRepository()
        {
            _bugs = new List<Bug>();

            _bugs.Add(new Bug
            {
                Id = 1,
                Title = "Bug 1",
                ProjectName = "Project A",
                Priority = "high",
                Status = "open",
                CreatedBy = "User1",
                CreatedOn = DateTime.Now.AddDays(-10)
            });

            _bugs.Add(new Bug
            {
                Id = 2,
                Title = "Bug 2",
                ProjectName = "Project B",
                Priority = "medium",
                Status = "closed",
                CreatedBy = "User2",
                CreatedOn = DateTime.Now.AddDays(-5)
            });

            _bugs.Add(new Bug
            {
                Id = 3,
                Title = "Bug 3",
                ProjectName = "Project A",
                Priority = "low",
                Status = "inprogress",
                CreatedBy = "User1",
                CreatedOn = DateTime.Now.AddDays(-2)
            });

            _bugs.Add(new Bug
            {
                Id = 4,
                Title = "Bug 4",
                ProjectName = "Project C",
                Priority = "high",
                Status = "open",
                CreatedBy = "User3",
                CreatedOn = DateTime.Now.AddDays(-1)
            });

            _bugs.Add(new Bug
            {
                Id = 5,
                Title = "Bug 5",
                ProjectName = "Project B",
                Priority = "medium",
                Status = "closed",
                CreatedBy = "User2",
                CreatedOn = DateTime.Now.AddDays(-3)
            });
        }

        public List<Bug> GetAllBugs()
        {
            return _bugs;
        }
    }
}
