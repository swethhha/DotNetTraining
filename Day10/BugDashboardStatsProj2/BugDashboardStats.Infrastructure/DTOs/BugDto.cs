namespace BugDashboardStats.Infrastructure.DTOs
{
    public class BugDto
    {
        public required string Title { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Project { get; set; } = string.Empty;
        public string Creator { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
