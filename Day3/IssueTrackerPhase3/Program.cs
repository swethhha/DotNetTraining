using System;
using System.Collections.Generic;
using IssueTrackerPhase3.Models;
using IssueTrackerPhase3.Services;

namespace IssueTrackerPhase3
{
    class Program
    {
        static void Main(string[] args)
        {
            var bug = new Bug(1, "Crash on login", "App crashes after entering credentials", "High");
            var task = new IssueTrackerPhase3.Models.Task(2, "Refactor module", "Refactor user module for performance", "DevTeam");
            var feature = new FeatureRequest(3, "Dark Mode", "Add support for dark theme", "UI");

            bug.Close();
            task.Reopen();
            feature.Close();

            var issues = new List<Issue> { bug, task, feature };
            var reportables = new List<IReportable> { bug, task, feature };

            IIssueService service = new IssueService();
            service.DisplayAll(issues);
            service.ShowReports(reportables);
        }
    }
}
