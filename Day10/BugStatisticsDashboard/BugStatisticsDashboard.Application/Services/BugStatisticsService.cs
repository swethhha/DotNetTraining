using BugStatisticsDashboard.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugStatisticsDashboard.Application.Services
{
    public class BugStatisticsService
    {
        
        private readonly IBugRepository _bugRepository;
        public BugStatisticsService(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }
        public void ShowBugCountByStatus()
        {
            var bugs = _bugRepository.GetAllBugs();
            var bugCountByStatus = bugs.GroupBy(b => b.Status)
                                       .Select(g => new { Status = g.Key, Count = g.Count() })
                                       .ToList();
            Console.WriteLine("Bug Count by Status:");
            foreach (var item in bugCountByStatus)
            {
                Console.WriteLine($"{item.Status}: {item.Count}");
            }
        }
        public void ShowBugCountByProjectAndPriority()
        {
            var bugs = _bugRepository.GetAllBugs();
            var bugCountByProjectAndPriority = bugs.GroupBy(b => new { b.ProjectName, b.Priority })
                                                   .Select(g => new { g.Key.ProjectName, g.Key.Priority, Count = g.Count() })
                                                   .ToList();
            Console.WriteLine("Bug Count by Project and Priority:");
            foreach (var item in bugCountByProjectAndPriority)
            {
                Console.WriteLine($"{item.ProjectName} - {item.Priority}: {item.Count}");
            }
        }
        public void ShowDailyBugReport()
        {
            var bugs = _bugRepository.GetAllBugs();
            var dailyBugReport = bugs.GroupBy(b => b.CreatedOn.Date)
                                     .Select(g => new { Date = g.Key, Count = g.Count() })
                                     .OrderBy(g => g.Date)
                                     .ToList();
            Console.WriteLine("Daily Bug Report:");
            foreach (var item in dailyBugReport)
            {
                Console.WriteLine($"{item.Date.ToShortDateString()}: {item.Count}");
            }
        }
        public void ShowTopCreators()
        {
            var bugs = _bugRepository.GetAllBugs();
            var topCreators = bugs.GroupBy(b => b.CreatedBy)
                                  .Select(g => new { Creator = g.Key, Count = g.Count() })
                                  .OrderByDescending(g => g.Count)
                                  .Take(5)
                                  .ToList();
            Console.WriteLine("Top 5 Bug Creators:");
            foreach (var item in topCreators)
            {
                Console.WriteLine($"{item.Creator}: {item.Count}");
            }
        }
    }
}
