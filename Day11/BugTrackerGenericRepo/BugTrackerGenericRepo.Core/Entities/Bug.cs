using System;

namespace BugTrackerGenericRepo.Core.Entities
{
    public class Bug
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Open"; // Default status
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Priority { get; set; } = "Medium"; // Default priority
    }
}
