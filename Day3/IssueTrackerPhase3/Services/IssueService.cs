using System;
using System.Collections.Generic;
using IssueTrackerPhase3.Models;

namespace IssueTrackerPhase3.Services
{
    public class IssueService : IIssueService
    {
        public void DisplayAll(List<Issue> issues)
        {
            Console.WriteLine("\nAll Issues:");
            foreach (var issue in issues)
            {
                issue.Display();
            }
        }

        public void ShowReports(List<IReportable> reportables)
        {
            Console.WriteLine("\nReports:");
            foreach (var reportable in reportables)
            {
                Console.WriteLine(reportable.GenerateReport());
            }
        }
    }
}
