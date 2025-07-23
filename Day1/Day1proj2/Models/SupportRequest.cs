using System;

namespace Day1proj2.Models
{
    public class SupportRequest
    {
        public int RequestId { get; }
        public string Issue { get; }
        public string Status { get; private set; }
        public DateTime CreatedOn { get; }
        public int ResolutionTimeInHours { get; private set; }
        public bool IsResolved { get; private set; }
        public SupportAgent AssignedTo { get; private set; }

        public SupportRequest(int requestId, string issue, SupportAgent assignedTo)
        {
            RequestId = requestId;
            Issue = issue;
            AssignedTo = assignedTo;
            Status = "Open";
            IsResolved = false;
            ResolutionTimeInHours = 0;
            CreatedOn = DateTime.Now;
        }

        public void MarkResolved()
        {
            if (!IsResolved)
            {
                IsResolved = true;
                Status = "Closed";
                ResolutionTimeInHours = (int)(DateTime.Now - CreatedOn).TotalHours;
            }
        }

        public void Reassign(SupportAgent newAgent)
        {
            AssignedTo = newAgent;
        }

        public void DisplaySummary()
        {
            Console.WriteLine($"Request ID: {RequestId}");
            Console.WriteLine($"Issue: {Issue}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Created On: {CreatedOn}");
            Console.WriteLine($"Resolution Time (hrs): {ResolutionTimeInHours}");
            Console.WriteLine($"Is Resolved: {IsResolved}");
            Console.WriteLine($"Assigned To: {AssignedTo.Name} (Dept: {AssignedTo.Department})");
            Console.WriteLine(new string('-', 40));
        }
    }
}
