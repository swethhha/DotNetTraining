using System;

namespace Day2Proj2SupportDeskPro.Models
{
    public class BugReport : SupportTicket, IReportable
    {
        public string Severity { get; set; }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Severity: {Severity}");
        }

        public void ReportStatus()
        {
            Console.WriteLine($"Bug Report [{Id}] is '{Status}' with severity '{Severity}'.");
        }
    }
}
