using System;

namespace Day1proj1phase2.Models
{
    public class Ticket
    {
        public int TicketId { get; }
        public string Title { get; }
        public string Priority { get; }
        public User AssignedTo { get; private set; }
        public string Status { get; private set; }
        public DateTime DateCreated { get; }

        public Ticket(int id, string title, string priority, User assignedTo)
        {
            TicketId = id;
            Title = title;
            Priority = priority;
            AssignedTo = assignedTo;
            Status = "Open";
            DateCreated = DateTime.Now;
        }

        public void ReassignTicket(User newAdmin)
        {
            AssignedTo = newAdmin;
        }

        public void CloseTicket()
        {
            Status = "Closed";
        }

        public void DisplaySummary()
        {
            Console.WriteLine($"\nTicket #{TicketId}: {Title}");
            Console.WriteLine($"Status: {Status}, Priority: {Priority}");
            Console.WriteLine($"Assigned to: {AssignedTo.Name}");
            Console.WriteLine($"Created On: {DateCreated}");
        }
    }
}
