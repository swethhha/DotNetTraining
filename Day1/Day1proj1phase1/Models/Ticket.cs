using System;

namespace Day1proj1phase1.Models
{
    public class Ticket
    {
        public int TicketId { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; private set; }
        public User CreatedBy { get; set; }

        public Ticket(int id, string title, string description, User createdBy)
        {
            TicketId = id;
            Title = title;
            Description = description;
            Status = "Open";
            CreatedBy = createdBy;
        }

        public void CloseTicket()
        {
                Status = "Closed";

        }

        public void DisplayTicket()
        {
            Console.WriteLine($"Ticket ID: {TicketId}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Created By: {CreatedBy.Name} ({CreatedBy.Roll})");
        }
    }
}
