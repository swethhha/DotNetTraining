using System;

namespace IssueTrackerPhase3.Models
{
    public class Task : Issue, IReportable
    {
        public string AssignedTo { get; private set; }

        public Task(int id, string title, string description, string assignedTo)
            : base(id, title, description)
        {
            AssignedTo = assignedTo;
        }

        public override void Close()
        {
            Status = "Closed";
            Console.WriteLine($"Task ID {Id} assigned to {AssignedTo} has been closed.");
        }

        public override void Reopen()
        {
            Status = "Reopened";
            Console.WriteLine($"Task ID {Id} assigned to {AssignedTo} has been reopened.");
        }

        public override void Display()
        {
            Console.WriteLine($"[Task] ID: {Id}, Title: {Title}, Description: {Description}, Assigned To: {AssignedTo}, Status: {Status}");
        }

        public string GenerateReport()
        {
            return $"Task Report - ID: {Id}, Title: {Title}, Assigned To: {AssignedTo}, Status: {Status}";
        }
    }
}
