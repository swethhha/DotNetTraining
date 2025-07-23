using System;

namespace Day2Proj2SupportDeskPro.Models
{
    public class SupportTicket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; } = "Open";

        public virtual void DisplayDetails()
        {
            Console.WriteLine($"Ticket ID: {Id}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Created By: {CreatedBy}");
            Console.WriteLine($"Status: {Status}");
        }

        public void CloseTicket()
        {
            Status = "Closed";
        }
    }
}
