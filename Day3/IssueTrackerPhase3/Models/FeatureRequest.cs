using System;

namespace IssueTrackerPhase3.Models
{
    public class FeatureRequest : Issue, IReportable
    {
        public string RequestedFeatureArea { get; private set; }

        public FeatureRequest(int id, string title, string description, string requestedFeatureArea)
            : base(id, title, description)
        {
            RequestedFeatureArea = requestedFeatureArea;
        }

        public override void Close()
        {
            Status = "Closed";
            Console.WriteLine($"Feature Request ID {Id} for {RequestedFeatureArea} has been closed.");
        }

        public override void Reopen()
        {
            Status = "Reopened";
            Console.WriteLine($"Feature Request ID {Id} for {RequestedFeatureArea} has been reopened.");
        }

        public override void Display()
        {
            Console.WriteLine($"[Feature] ID: {Id}, Title: {Title}, Description: {Description}, Area: {RequestedFeatureArea}, Status: {Status}");
        }

        public string GenerateReport()
        {
            return $"Feature Report - ID: {Id}, Title: {Title}, Area: {RequestedFeatureArea}, Status: {Status}";
        }
    }
}
