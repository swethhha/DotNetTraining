using System;

namespace IssueTrackerPhase3.Models
{
    public abstract class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; protected set; }

        protected Issue(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            Status = "Open";
        }

        public abstract void Close();
        public abstract void Reopen();
        public abstract void Display();
    }
}
