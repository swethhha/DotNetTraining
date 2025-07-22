// Program.cs

using System;
using Day1proj1phase1.Models;

namespace Day1proj1phase1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User(1, "Alice", "Admin");
            Ticket ticket = new Ticket(101, "Login Issue", "Unable to login with valid credentials.", user);

            user.DisplayUser();
            ticket.DisplayTicket();
            ticket.CloseTicket();
            ticket.DisplayTicket(); // Display ticket after closing it

            Console.ReadLine(); // Optional: pause the console
        }
    }
}
