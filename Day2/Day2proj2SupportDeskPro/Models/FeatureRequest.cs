using System;

namespace Day2Proj2SupportDeskPro.Models
{
    public class FeatureRequest : SupportTicket, IReportable
    {
        public string RequestedBy { get; set; }
        public DateTime ETA { get; set; }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Requested By: {RequestedBy}");
            Console.WriteLine($"ETA: {ETA.ToShortDateString()}");
        }

        public void ReportStatus()
        {
            Console.WriteLine($"Feature Request [{Id}] is '{Status}', requested by {RequestedBy}, ETA: {ETA.ToShortDateString()}.");
        }
    }
}
