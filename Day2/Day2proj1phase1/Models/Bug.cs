using System;

namespace Day2proj1phase1.Models
{
    public class Bug : Issue, IReportable
    {
        public string Severity { get; set; }

        public Bug(int id, string title, string assignedTo, string severity)
            : base(id, title, assignedTo)
        {
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
            Console.WriteLine($"Bug #{Id} is under investigation. Severity - {Severity}");
        }
    }
}
