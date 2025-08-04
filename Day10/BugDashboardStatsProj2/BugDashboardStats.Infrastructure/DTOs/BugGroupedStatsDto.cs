namespace BugDashboardStats.Infrastructure.DTOs
{
    public class BugGroupedStatsDto
    {
        public string Key { get; set; } = string.Empty;  // e.g. "High", "Open", or "ProjectA"
        public int Count { get; set; }
    }
}
