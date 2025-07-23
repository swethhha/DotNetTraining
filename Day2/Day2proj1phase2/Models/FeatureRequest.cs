using System;

namespace Day2proj1phase2.Models
{
    public class FeatureRequest : Issue, IReportable
    {
        public string RequestedBy { get; }
        public DateTime PlannedReleaseDate { get; }

        public FeatureRequest(int id, string title, string assignedTo, string requestedBy, DateTime plannedReleaseDate)
            : base(id, title, assignedTo)
        {
            if (requestedBy == null || requestedBy.Trim() == "")
            {
                Console.WriteLine("RequestedBy cannot be empty.");
            }
            RequestedBy = requestedBy;
            PlannedReleaseDate = plannedReleaseDate;
        }

        public override void Display()
        {
            Console.Write("[FeatureRequest] ");
            base.Display();
            Console.WriteLine($"Requested By: {RequestedBy}, Planned Release: {PlannedReleaseDate:d}");
        }

        public void ReportStatus()
        {
            Console.WriteLine($"FeatureRequest #{Id} [{Status}] - Requested By: {RequestedBy}");
        }

        public string GetSummary()
        {
            return $"FeatureRequest #{Id}: {Title} - Status: {Status} - Assigned To: {AssignedTo}";
        }
    }
}
