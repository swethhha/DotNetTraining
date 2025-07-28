using IssueTracker.Application.Interfaces;
using IssueTracker.Application.Services;
using IssueTracker.Core.Entities;
using IssueTracker.Core.Interfaces;
using IssueTracker.Infrastructure.Repositories;
using System;

namespace IssueTracker.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IBugRepository repository = new BugRepository();
            IBugService service = new BugService(repository);

            while (true)
            {
                Console.WriteLine("\n--- Bug Tracker Menu ---");
                Console.WriteLine("1. Add Bug");
                Console.WriteLine("2. View All Bugs");
                Console.WriteLine("3. Update Bug");
                Console.WriteLine("4. Delete Bug");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Title: ");
                        string title = Console.ReadLine() ?? "";
                        if (string.IsNullOrWhiteSpace(title))
                        {
                            Console.WriteLine("Title cannot be empty.");
                            break;
                        }

                        Console.Write("Enter Description: ");
                        string description = Console.ReadLine() ?? "";
                        if (string.IsNullOrWhiteSpace(description))
                        {
                            Console.WriteLine("Description cannot be empty.");
                            break;
                        }

                        service.Add(new Bug { Title = title, Description = description });
                        Console.WriteLine("Bug added successfully!");
                        break;

                    case "2":
                        var bugs = service.GetAll();
                        if (bugs.Count == 0)
                        {
                            Console.WriteLine("No bugs found.");
                        }
                        else
                        {
                            Console.WriteLine("\nBug List:");
                            foreach (var bug in bugs)
                            {
                                Console.WriteLine($"ID: {bug.Id}, Title: {bug.Title}, Desc: {bug.Description}, Status: {bug.Status}");
                            }
                        }
                        break;

                    case "3":
                        Console.Write("Enter Bug ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            var bugToUpdate = service.GetById(updateId);
                            if (bugToUpdate == null)
                            {
                                Console.WriteLine("Bug not found.");
                                break;
                            }

                            Console.Write("New Title: ");
                            string newTitle = Console.ReadLine() ?? "";
                            if (string.IsNullOrWhiteSpace(newTitle))
                            {
                                Console.WriteLine("Title cannot be empty.");
                                break;
                            }

                            Console.Write("New Description: ");
                            string newDescription = Console.ReadLine() ?? "";
                            if (string.IsNullOrWhiteSpace(newDescription))
                            {
                                Console.WriteLine("Description cannot be empty.");
                                break;
                            }

                            Console.Write("New Status: ");
                            string newStatus = Console.ReadLine() ?? "";
                            if (string.IsNullOrWhiteSpace(newStatus))
                            {
                                Console.WriteLine("Status cannot be empty.");
                                break;
                            }

                            bugToUpdate.Title = newTitle;
                            bugToUpdate.Description = newDescription;
                            bugToUpdate.Status = newStatus;

                            service.Update(bugToUpdate);
                            Console.WriteLine("Bug updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID.");
                        }
                        break;

                    case "4":
                        Console.Write("Enter Bug ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            var existingBug = service.GetById(deleteId);
                            if (existingBug == null)
                            {
                                Console.WriteLine("Bug not found.");
                                break;
                            }

                            service.Delete(deleteId);
                            Console.WriteLine("Bug deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID.");
                        }
                        break;

                    case "5":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
