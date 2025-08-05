using BugTrackerGenericRepo.Application.Services;
using BugTrackerGenericRepo.Core.Entities;
using BugTrackerGenericRepo.Infrastructure.Repositories;

namespace BugTrackerGenericRepo.ConsoleUI
{
    class Program
    {
        static int projectId = 1;
        static int userId = 1;
        static int bugId = 1;

        static void Main(string[] args)
        {
            var projectService = new ProjectService(new ProjectRepository());
            var userService = new UserService(new UserRepository());
            var bugService = new BugService(new BugRepository());

            while (true)
            {
                Console.WriteLine("\n=== Bug Tracker Console Menu ===");
                Console.WriteLine("1. Add Project");
                Console.WriteLine("2. View Projects");
                Console.WriteLine("3. Update Project");
                Console.WriteLine("4. Delete Project");
                Console.WriteLine("5. Add User");
                Console.WriteLine("6. View Users");
                Console.WriteLine("7. Update User");
                Console.WriteLine("8. Delete User");
                Console.WriteLine("9. Add Bug");
                Console.WriteLine("10. View Bugs");
                Console.WriteLine("11. Update Bug");
                Console.WriteLine("12. Delete Bug");
                Console.WriteLine("13. Exit");
                Console.Write("Choose option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Project Name: ");
                        var pname = ReadNonEmpty();
                        projectService.CreateProject(new Project { ProjectId = projectId++, ProjectName = pname });
                        Console.WriteLine("Project added successfully.");
                        break;

                    case "2":
                        var projects = projectService.GetAllProjects();
                        if (projects.Count == 0) Console.WriteLine("No projects found.");
                        else foreach (var p in projects)
                                Console.WriteLine($"ID: {p.ProjectId}, Name: {p.ProjectName}");
                        break;

                    case "3":
                        Console.Write("Enter Project ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int pid))
                        {
                            Console.Write("Enter new Project Name: ");
                            var newPname = ReadNonEmpty();
                            projectService.UpdateProject(new Project { ProjectId = pid, ProjectName = newPname });
                            Console.WriteLine("Project updated.");
                        }
                        break;

                    case "4":
                        Console.Write("Enter Project ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int delPid))
                        {
                            projectService.DeleteProject(delPid);
                            Console.WriteLine("Project deleted.");
                        }
                        break;

                    case "5":
                        Console.Write("Enter Username: ");
                        var uname = ReadNonEmpty();
                        userService.AddUser(new User { UserId = userId++, Username = uname });
                        Console.WriteLine("User added.");
                        break;

                    case "6":
                        var users = userService.GetAllUsers();
                        if (users.Count == 0) Console.WriteLine("No users found.");
                        else foreach (var u in users)
                                Console.WriteLine($"ID: {u.UserId}, Username: {u.Username}");
                        break;

                    case "7":
                        Console.Write("Enter User ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int uid))
                        {
                            Console.Write("Enter new Username: ");
                            var newUname = ReadNonEmpty();
                            userService.UpdateUser(new User { UserId = uid, Username = newUname });
                            Console.WriteLine("User updated.");
                        }
                        break;

                    case "8":
                        Console.Write("Enter User ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int delUid))
                        {
                            userService.DeleteUser(delUid);
                            Console.WriteLine("User deleted.");
                        }
                        break;

                    case "9":
                        var newBug = new Bug();
                        newBug.Id = bugId++;

                        Console.Write("Enter Bug Title: ");
                        newBug.Title = ReadNonEmpty();

                        Console.Write("Enter Bug Description: ");
                        newBug.Description = ReadNonEmpty();

                        Console.Write("Enter Bug Status (Open/In Progress/Closed): ");
                        newBug.Status = ReadFromOptions(new[] { "Open", "In Progress", "Closed" });

                        Console.Write("Enter Bug Priority (Low/Medium/High): ");
                        newBug.Priority = ReadFromOptions(new[] { "Low", "Medium", "High" });

                        newBug.CreatedAt = DateTime.Now;
                        bugService.CreateBug(newBug);
                        Console.WriteLine("Bug created.");
                        break;

                    case "10":
                        var bugs = bugService.GetAllBugs();
                        if (bugs.Count == 0) Console.WriteLine("No bugs found.");
                        else foreach (var b in bugs)
                            {
                                Console.WriteLine($"ID: {b.Id}");
                                Console.WriteLine($"Title: {b.Title}");
                                Console.WriteLine($"Description: {b.Description}");
                                Console.WriteLine($"Status: {b.Status}");
                                Console.WriteLine($"Priority: {b.Priority}");
                                Console.WriteLine($"Created At: {b.CreatedAt}");
                                Console.WriteLine("-----------------------------");
                            }
                        break;

                    case "11":
                        Console.Write("Enter Bug ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int bid))
                        {
                            Console.Write("Enter new Title: ");
                            var newTitle = ReadNonEmpty();

                            Console.Write("Enter new Description: ");
                            var newDesc = ReadNonEmpty();

                            Console.Write("Enter new Status (Open/In Progress/Closed): ");
                            var newStatus = ReadFromOptions(new[] { "Open", "In Progress", "Closed" });

                            Console.Write("Enter new Priority (Low/Medium/High): ");
                            var newPriority = ReadFromOptions(new[] { "Low", "Medium", "High" });

                            bugService.UpdateBug(new Bug
                            {
                                Id = bid,
                                Title = newTitle,
                                Description = newDesc,
                                Status = newStatus,
                                Priority = newPriority,
                                CreatedAt = DateTime.Now
                            });

                            Console.WriteLine("Bug updated.");
                        }
                        break;

                    case "12":
                        Console.Write("Enter Bug ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int delBid))
                        {
                            bugService.DeleteBug(delBid);
                            Console.WriteLine("Bug deleted.");
                        }
                        break;

                    case "13":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static string ReadNonEmpty()
        {
            string? input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Input cannot be empty. Try again: ");
                input = Console.ReadLine();
            }
            return input;
        }

        static string ReadFromOptions(string[] options)
        {
            string? input = Console.ReadLine();
            while (!options.Contains(input, StringComparer.OrdinalIgnoreCase))
            {
                Console.Write($"Invalid option. Choose ({string.Join("/", options)}): ");
                input = Console.ReadLine();
            }
            return input!;
        }
    }
}
