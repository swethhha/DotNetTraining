namespace BugDashboardStats.Core.Entities
{
    public class Bug
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public DateTime CreatedDate { get; set; }

        public required Project Project { get; set; }
        public required User Creator { get; set; }
    }
}
