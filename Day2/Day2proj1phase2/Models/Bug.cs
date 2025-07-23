namespace Day2proj1phase2.Models
{
    public class Bug : Issue, IReportable
    {
        public string Severity { get; }

        public Bug(int id, string title, string assignedTo, string severity)
            : base(id, title, assignedTo)
        {
            if (severity == null || severity.Trim() == "")
            {
                Console.WriteLine("Severity cannot be empty.");
            }
            Severity = severity;
        }

        public override void Display()
        {
            Console.Write("[Bug] ");
            base.Display();
            Console.WriteLine($"Severity: {Severity}");
        }

        public void ReportStatus()
        {
            Console.WriteLine($"Bug #{Id} [{Status}] - Severity: {Severity}");
        }

        public string GetSummary()
        {
            return $"Bug #{Id}: {Title} - Status: {Status} - Assigned To: {AssignedTo}";
        }
    }
}
