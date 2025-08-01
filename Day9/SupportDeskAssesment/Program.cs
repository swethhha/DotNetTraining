using System;
using System.Linq;
using SupportDeskAssesment.Models;
using SupportDeskAssesment.Data;

class Program
{
    static void Main(string[] args)
    {
        var context = new AppDbContext();

        var userService = new UserService(context);
        var tagService = new TagService(context);
        var ticketService = new TicketService(context);

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== Support Desk System ===");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. View All Users");
            Console.WriteLine("3. Add Tag");
            Console.WriteLine("4. View All Tags");
            Console.WriteLine("5. Create Ticket");
            Console.WriteLine("6. View All Tickets");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter user name: ");
                    var name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("User name cannot be empty.");
                        break;
                    }
                    userService.AddUser(name);
                    Console.WriteLine("User added.");
                    break;

                case "2":
                    var users = userService.GetAllUsers();
                    Console.WriteLine("Users:");
                    foreach (var u in users)
                        Console.WriteLine($"[{u.UserId}] {u.UserName}");
                    break;

                case "3":
                    Console.Write("Enter tag name: ");
                    var tagName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(tagName))
                    {
                        Console.WriteLine("Tag name cannot be empty.");
                        break;
                    }
                    tagService.AddTag(tagName);
                    Console.WriteLine("Tag added.");
                    break;

                case "4":
                    var tags = tagService.GetAllTags();
                    Console.WriteLine("Tags:");
                    foreach (var t in tags)
                        Console.WriteLine($"[{t.TagId}] {t.TagName}");
                    break;

                case "5":
                    Console.Write("Title: ");
                    var title = Console.ReadLine();
                    Console.Write("Description: ");
                    var desc = Console.ReadLine();
                    Console.Write("Enter User ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int uid))
                    {
                        Console.WriteLine("Invalid User ID.");
                        break;
                    }

                    var allTags = tagService.GetAllTags();
                    Console.WriteLine("Available Tags:");
                    foreach (var t in allTags)
                        Console.WriteLine($"[{t.TagId}] {t.TagName}");

                    Console.Write("Enter comma-separated Tag IDs: ");
                    var tagInput = Console.ReadLine();
                    var tagIds = tagInput.Split(',')
                        .Select(id => int.TryParse(id.Trim(), out int tid) ? tid : (int?)null)
                        .Where(tid => tid.HasValue)
                        .Select(tid => tid.Value)
                        .ToList();

                    if (tagIds.Count == 0)
                    {
                        Console.WriteLine("No valid Tag IDs entered.");
                        break;
                    }

                    ticketService.CreateTicket(title, desc, uid, tagIds);
                    Console.WriteLine("Ticket created.");
                    break;

                case "6":
                    var tickets = ticketService.GetAllTickets();
                    Console.WriteLine("Tickets:");
                    foreach (var t in tickets)
                    {
                        Console.WriteLine($"\n[{t.TicketId}] {t.Title}");
                        Console.WriteLine($"User     : {t.User?.UserName ?? "N/A"}");
                        Console.WriteLine($"Status   : {t.Status}");
                        Console.WriteLine($"Tags     : {string.Join(", ", t.TicketTags.Select(tt => tt.Tag?.TagName ?? "N/A"))}");
                        Console.WriteLine($"Desc     : {t.Description}");
                    }
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Exiting. See you next time!");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
