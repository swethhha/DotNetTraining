using System;

namespace Day2proj1phase1.Models
{
    public class Task : Issue, IReportable
    {
        public int EstimatedHours { get; set; }

        public Task(int id, string title, string assignedTo, int estimatedHours)
            : base(id, title, assignedTo)
        {
            EstimatedHours = estimatedHours;
        }

        public override void Display()
        {
            Console.Write("[Task] ");
            base.Display();
            Console.WriteLine($"Estimated Hours: {EstimatedHours}");
        }

        public void ReportStatus()
        {
            Console.WriteLine($"Task #{Id} is in progress. Estimated Hours - {EstimatedHours}");
        }
    }
}
