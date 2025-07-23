using System;
using Day1proj1phase2.Models;

namespace Day1proj1phase2
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Enter your name: ");
            var user = new User(1, Console.ReadLine());

            var admin1 = new User(101, "Admin1");
            var admin2 = new User(102, "Admin2");

            Random rnd = new Random();
            int ticketId = rnd.Next(1000, 9999);
            Console.Write("Enter issue title: ");
            string title = Console.ReadLine();
            Console.Write("Enter priority (Low, Medium, High): ");
            string priority = Console.ReadLine();

            var ticket = new Ticket(ticketId, title, priority, admin1);
            Console.WriteLine("\nTicket Created:");
            ticket.DisplaySummary();

            Console.Write("\nAdmin1: Can you resolve this? (yes/no): ");
            string canResolve = Console.ReadLine().ToLower();

            if (canResolve == "no")
            {
                ticket.ReassignTicket(admin2);
                Console.WriteLine("⤴️ Ticket reassigned to Admin2.");
            }

            Console.Write("Is the issue resolved? (yes/no): ");
            string resolved = Console.ReadLine().ToLower();
            if (resolved == "yes")
            {
                ticket.CloseTicket();
                Console.WriteLine(" Ticket closed.");
            }

            Console.WriteLine("\nFinal Ticket Summary:");
            ticket.DisplaySummary();
        }
    }
}
