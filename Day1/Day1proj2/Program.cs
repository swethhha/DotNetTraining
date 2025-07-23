using System;
using Day1proj2.Models;

namespace Day1proj2
{
    internal class Program
    {
        static void Main()
        {

            var agent1 = new SupportAgent(1, "Swetha", "Technical");
            var agent2 = new SupportAgent(2, "Mirdula", "Billing");

            agent1.DisplayAgent();
            agent2.DisplayAgent();
            Console.WriteLine();

            var request1 = new SupportRequest(101, "Cannot access account", agent1);
            var request2 = new SupportRequest(102, "Incorrect billing amount", agent2);

            Console.WriteLine("Initial Requests Summary:");
            request1.DisplaySummary();
            request2.DisplaySummary();

            Console.WriteLine("Marking Request 1 as resolved...");
            request1.MarkResolved();

            Console.WriteLine("Reassigning Request 2 to Alice...");
            request2.Reassign(agent1);

            Console.WriteLine("Updated Requests Summary:");
            request1.DisplaySummary();
            request2.DisplaySummary();

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
