using BugDashboardStats.Application.Services;
using BugDashboardStats.Infrastructure.Repositories;
using BugDashboardStats.Infrastructure.DTOs;

class Program
{
    static void Main()
    {
        var repo = new BugRepository();
        var service = new BugService(repo);

        while (true)
        {
            Console.WriteLine("\n=== Bug Dashboard Menu ===");
            Console.WriteLine("1. View All Bugs");
            Console.WriteLine("2. Filter by Status / Priority / Project");
            Console.WriteLine("3. Sort by Created Date");
            Console.WriteLine("4. Show Grouped Statistics");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option (1-5): ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowBugs(service.GetAllBugs());
                    break;

                case "2":
                    Console.Write("Filter by (status / priority / project): ");
                    var filterType = Console.ReadLine()?.Trim().ToLower();
                    Console.Write("Enter value to filter: ");
                    var value = Console.ReadLine()?.Trim();

                    List<BugDto> filtered = filterType switch
                    {
                        "status" => service.GetAllBugs().Where(b => b.Status.Equals(value, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "priority" => service.GetAllBugs().Where(b => b.Priority.Equals(value, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "project" => service.GetAllBugs().Where(b => b.Project.Equals(value, StringComparison.OrdinalIgnoreCase)).ToList(),
                        _ => new List<BugDto>()
                    };

                    ShowBugs(filtered);
                    break;

                case "3":
                    var sorted = service.GetAllBugs()
                        .OrderByDescending(b => b.CreatedDate)
                        .ToList();
                    ShowBugs(sorted);
                    break;

                case "4":
                    Console.WriteLine("\n--- Bug Count by Status ---");
                    ShowStats(service.GetBugCountByStatus());

                    Console.WriteLine("\n--- Bug Count by Priority ---");
                    ShowStats(service.GetBugCountByPriority());

                    Console.WriteLine("\n--- Bug Count by Project ---");
                    ShowStats(service.GetBugCountByProject());
                    break;

                case "5":
                    Console.WriteLine("Exiting application. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void ShowBugs(List<BugDto> bugs)
    {
        Console.WriteLine("\n--- Bug List ---");
        if (bugs.Count == 0)
        {
            Console.WriteLine("No bugs found.");
            return;
        }

        foreach (var bug in bugs)
        {
            Console.WriteLine($"Title     : {bug.Title}");
            Console.WriteLine($"Status    : {bug.Status}");
            Console.WriteLine($"Priority  : {bug.Priority}");
            Console.WriteLine($"Project   : {bug.Project}");
            Console.WriteLine($"Creator   : {bug.Creator}");
            Console.WriteLine($"Created On: {bug.CreatedDate:d}");
            Console.WriteLine(new string('-', 40));
        }
    }

    static void ShowStats(List<BugGroupedStatsDto> stats)
    {
        if (stats.Count == 0)
        {
            Console.WriteLine("No data available.");
            return;
        }

        foreach (var stat in stats)
        {
            Console.WriteLine($"{stat.Key}: {stat.Count}");
        }
    }
}
