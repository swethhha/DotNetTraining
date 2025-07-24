using System;

namespace IssueTrackerPhase3.Models
{
    public class Bug : Issue, IReportable
    {
        public string Severity { get; private set; }

        public Bug(int id, string title, string description, string severity)
            : base(id, title, description)
        {
            Severity = severity;
        }

        public override void Close()
        {
            Status = "Closed";
            Console.WriteLine($"Bug ID {Id} with severity {Severity} has been closed.");
        }

        public override void Reopen()
        {
            Status = "Reopened";
            Console.WriteLine($"Bug ID {Id} with severity {Severity} has been reopened.");
        }

        public override void Display()
        {
            Console.WriteLine($"[Bug] ID: {Id}, Title: {Title}, Description: {Description}, Severity: {Severity}, Status: {Status}");
        }

        public string GenerateReport()
        {
            return $"Bug Report - ID: {Id}, Title: {Title}, Severity: {Severity}, Status: {Status}";
        }
    }
}
