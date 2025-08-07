using AutoMapper;
using BugTracker.Application.Mapping;
using BugTracker.Application.Services;
using BugTracker.Core.DTOs;
using BugTracker.Core.Interfaces;
using BugTracker.Infrastructure.Repositories;

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
var mapper = config.CreateMapper();

// Repository layer
IBugRepository bugRepository = new BugRepository();
IUserRepository userRepository = new UserRepository();
IProjectRepository projectRepository = new ProjectRepository();

// Service layer
IBugService bugService = new BugService(bugRepository, mapper);
IUserService userService = new UserService(userRepository, mapper);
IProjectService projectService = new ProjectService(projectRepository, mapper);

bool exit = false;

while (!exit)
{
    Console.WriteLine("\n--- Bug Tracker Menu ---");
    Console.WriteLine("1. Add Bug");
    Console.WriteLine("2. View All Bugs");
    Console.WriteLine("3. Get Bug by ID");
    Console.WriteLine("4. Update Bug");
    Console.WriteLine("5. Delete Bug");
    Console.WriteLine("6. Add User");
    Console.WriteLine("7. View All Users");
    Console.WriteLine("8. Update User");
    Console.WriteLine("9. Delete User");
    Console.WriteLine("10. Add Project");
    Console.WriteLine("11. View All Projects");
    Console.WriteLine("12. Update Project");
    Console.WriteLine("13. Delete Project");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter Title: ");
            string title = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter Description: ");
            string description = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter Status: ");
            string status = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(status))
            {
                Console.WriteLine("All fields are required.");
                break;
            }

            var bugRequest = new BugRequestDTO
            {
                Title = title,
                Description = description,
                Status = status,
                DueDate = DateTime.Now
            };
            bugService.AddBug(bugRequest);
            Console.WriteLine("Bug added successfully.");
            break;

        case "2":
            var bugs = bugService.GetAllBugs();
            Console.WriteLine("--- All Bugs ---");
            if (!bugs.Any())
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (var bug in bugs)
                {
                    Console.WriteLine($"ID: {bug.Id}, Title: {bug.Title}, Status: {bug.Status}, Due Date: {bug.DueDate?.ToString("yyyy-MM-dd") ?? "N/A"}");
                }
            }
            break;

        case "3":
            Console.Write("Enter Bug ID: ");
            if (int.TryParse(Console.ReadLine(), out int bugId))
            {
                var foundBug = bugService.GetBugById(bugId);
                if (foundBug != null)
                    Console.WriteLine($"ID: {foundBug.Id}, Title: {foundBug.Title}, Status: {foundBug.Status}, Due Date: {foundBug.DueDate?.ToString("yyyy-MM-dd") ?? "N/A"}");
                else
                    Console.WriteLine("Bug not found.");
            }
            break;

        case "4":
            Console.Write("Enter Bug ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int updateBugId))
            {
                Console.Write("Enter Title: ");
                string newTitle = Console.ReadLine()?.Trim() ?? "";
                Console.Write("Enter Description: ");
                string newDescription = Console.ReadLine()?.Trim() ?? "";
                Console.Write("Enter Status: ");
                string newStatus = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(newTitle) || string.IsNullOrWhiteSpace(newDescription) || string.IsNullOrWhiteSpace(newStatus))
                {
                    Console.WriteLine("All fields are required.");
                    break;
                }

                var updateRequest = new BugRequestDTO
                {
                    Id = updateBugId,
                    Title = newTitle,
                    Description = newDescription,
                    Status = newStatus,
                    DueDate = DateTime.Now
                };
                bugService.UpdateBug(updateRequest);
                Console.WriteLine("Bug updated.");
            }
            break;

        case "5":
            Console.Write("Enter Bug ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int deleteBugId))
            {
                bugService.DeleteBug(deleteBugId);
                Console.WriteLine("Bug deleted.");
            }
            break;

        case "6":
            Console.Write("Enter User Name: ");
            string userName = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("User name cannot be empty.");
                break;
            }

            var userDto = new UserRequestDTO { Name = userName };
            userService.AddUser(userDto);
            Console.WriteLine("User added.");
            break;

        case "7":
            var users = userService.GetAllUsers();
            Console.WriteLine("--- All Users ---");
            if (!users.Any())
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Name: {user.Name}");
                }
            }
            break;

        case "8":
            Console.Write("Enter User ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int updateUserId))
            {
                Console.Write("Enter New Name: ");
                string newUserName = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(newUserName))
                {
                    Console.WriteLine("Name cannot be empty.");
                    break;
                }

                var updateUserDto = new UserRequestDTO { Name = newUserName };
                userService.UpdateUser(updateUserDto);
                Console.WriteLine("User updated.");
            }
            break;

        case "9":
            Console.Write("Enter User ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int deleteUserId))
            {
                userService.DeleteUser(deleteUserId);
                Console.WriteLine("User deleted.");
            }
            break;

        case "10":
            Console.Write("Enter Project Title: ");
            string projectTitle = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(projectTitle))
            {
                Console.WriteLine("Project title cannot be empty.");
                break;
            }

            var projectDto = new ProjectRequestDTO { Title = projectTitle };
            projectService.AddProject(projectDto);
            Console.WriteLine("Project added.");
            break;

        case "11":
            var projects = projectService.GetAllProjects();
            Console.WriteLine("--- All Projects ---");
            if (!projects.Any())
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (var project in projects)
                {
                    Console.WriteLine($"ID: {project.Id}, Title: {project.Title}");
                }
            }
            break;

        case "12":
            Console.Write("Enter Project ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int updateProjectId))
            {
                Console.Write("Enter New Title: ");
                string newProjectTitle = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(newProjectTitle))
                {
                    Console.WriteLine("Title cannot be empty.");
                    break;
                }

                var updateProjectDto = new ProjectRequestDTO {Title = newProjectTitle };
                projectService.UpdateProject(updateProjectDto);
                Console.WriteLine("Project updated.");
            }
            break;

        case "13":
            Console.Write("Enter Project ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int deleteProjectId))
            {
                projectService.DeleteProject(deleteProjectId);
                Console.WriteLine("Project deleted.");
            }
            break;

        case "0":
            exit = true;
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}
