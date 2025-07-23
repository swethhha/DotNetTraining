using System;
using System.Collections.Generic;
using Day2proj1phase2.Models;
using MyTask = Day2proj1phase2.Models.Task;

namespace Day2proj1phase2
{
    class Program
    {
        static void Main()
        {
            var bug = new Bug(1, "App crashes on login", "Swetha", "High");
            var task = new MyTask(2, "Update UI", "Mirdu", 5);
            var feature = new FeatureRequest(3, "Add Dark Mode", "Divya", "Ram", DateTime.Now.AddMonths(1));

            List<IReportable> issues = new List<IReportable>() { bug, task, feature };

            Console.WriteLine("=== Issues ===");
            foreach (var issue in issues)
            {
                issue.Display();
                issue.ReportStatus();
                Console.WriteLine(issue.GetSummary());
                Console.WriteLine();
            }

            Console.WriteLine("=== Advancing statuses ===");
            bug.AdvanceStatus("In Progress");
            task.AdvanceStatus("Closed");
            feature.AdvanceStatus("Open");

            Console.WriteLine("\n=== Updated Statuses ===");
            foreach (var issue in issues)
            {
                issue.ReportStatus();
            }

            Console.WriteLine("\n=== Status Summary ===");
            PrintStatusSummary(issues);

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }

        static void PrintStatusSummary(List<IReportable> issues)
        {
            int openCount = 0;
            int inProgressCount = 0;
            int closedCount = 0;

            foreach (var issue in issues)
            {
                if (issue is Issue baseIssue)
                {
                    if (baseIssue.Status == "Open") openCount++;
                    else if (baseIssue.Status == "In Progress") inProgressCount++;
                    else if (baseIssue.Status == "Closed") closedCount++;
                }
            }

            Console.WriteLine($"Open: {openCount}");
            Console.WriteLine($"In Progress: {inProgressCount}");
            Console.WriteLine($"Closed: {closedCount}");
        }
    }
}
