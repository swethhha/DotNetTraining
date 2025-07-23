using System;

namespace Day2proj1phase1.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AssignedTo { get; set; }

        public Issue(int id, string title, string assignedTo)
        {
            Id = id;
            Title = title;
            AssignedTo = assignedTo;
        }

        public virtual void Display()
        {
            Console.WriteLine($"ID: {Id}, Title: {Title}, Assigned To: {AssignedTo}");
        }
    }
}
