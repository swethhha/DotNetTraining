using System;

namespace Day2proj1phase2.Models
{
    public class Issue
    {
        public int Id { get; }
        public string Title { get; }
        public string AssignedTo { get; }
        public string Status { get; private set; }

        public Issue(int id, string title, string assignedTo)
        {
            if (title == null || title.Trim() == "")
            {
                Console.WriteLine("Title cannot be empty.");
            }
            if (assignedTo == null || assignedTo.Trim() == "")
            {
                Console.WriteLine("AssignedTo cannot be empty.");
            }

            Id = id;
            Title = title;
            AssignedTo = assignedTo;
            Status = "Open";
        }

        public virtual void Display()
        {
            Console.WriteLine($"ID: {Id}, Title: {Title}, Assigned To: {AssignedTo}, Status: {Status}");
        }

        public void AdvanceStatus(string newStatus)
        {
            if (newStatus == "Open" || newStatus == "In Progress" || newStatus == "Closed")
            {
                Status = newStatus;
            }
            else
            {
                Console.WriteLine($"Invalid status: {newStatus}");
            }
        }
    }
}
