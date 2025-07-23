namespace Day2proj1phase2.Models
{
    public class Task : Issue, IReportable
    {
        public int EstimatedHours { get; }

        public Task(int id, string title, string assignedTo, int estimatedHours)
            : base(id, title, assignedTo)
        {
            if (estimatedHours <= 0)
            {
                Console.WriteLine("Estimated Hours should be positive.");
            }
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
            Console.WriteLine($"Task #{Id} [{Status}] - Estimated Hours: {EstimatedHours}");
        }

        public string GetSummary()
        {
            return $"Task #{Id}: {Title} - Status: {Status} - Assigned To: {AssignedTo}";
        }
    }
}
