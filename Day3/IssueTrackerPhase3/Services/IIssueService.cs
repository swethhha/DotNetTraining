using System.Collections.Generic;
using IssueTrackerPhase3.Models;

namespace IssueTrackerPhase3.Services
{
    public interface IIssueService
    {
        void DisplayAll(List<Issue> issues);
        void ShowReports(List<IReportable> reportables);
    }
}
