using System;
using System.Collections.Generic;
using SupportDesk.Application.Services;
using SupportDesk.Infrastructure;

namespace SupportDeskProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SupportDeskDbContext();
            var ticketService = new TicketService(context);
            var tagService = new TagService(context);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nSupport Desk - Menu");
                Console.WriteLine("1. View All Tickets");
                Console.WriteLine("2. View Tickets With Users");
                Console.WriteLine("3. View Tickets With Tags");
                Console.WriteLine("4. View Tickets By User ID");
                Console.WriteLine("5. View Tickets By Tag ID");
                Console.WriteLine("6. View Tag Ticket Counts");
                Console.WriteLine("7. View Ticket Count By User");
                Console.WriteLine("8. Assign Tags To Ticket");
                Console.WriteLine("9. View Tickets With User and Tag Names");
                Console.WriteLine("0. Exit");
                Console.Write("Choose option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        foreach (var t in ticketService.GetAllTickets())
                            Console.WriteLine($"{t.Id}: {t.Title}");
                        break;

                    case "2":
                        foreach (var t in ticketService.GetAllTicketsWithUsers())
                            Console.WriteLine($"{t.Id}: {t.Title} - {t.User?.Name}");
                        break;

                    case "3":
                        foreach (var t in ticketService.GetAllTicketsWithTags())
                        {
                            Console.WriteLine($"\nTicket: {t.Title}");
                            foreach (var tag in t.Tags)
                                Console.WriteLine($"  - Tag: {tag.Name}");
                        }
                        break;

                    case "4":
                        Console.Write("Enter User ID: ");
                        int uid = int.Parse(Console.ReadLine());
                        foreach (var t in ticketService.GetTicketsByUserId(uid))
                            Console.WriteLine($"{t.Id}: {t.Title}");
                        break;

                    case "5":
                        Console.Write("Enter Tag ID: ");
                        int tid = int.Parse(Console.ReadLine());
                        foreach (var t in ticketService.GetTicketsByTagId(tid))
                            Console.WriteLine($"{t.Id}: {t.Title}");
                        break;

                    case "6":
                        foreach (var item in ticketService.GetTagTicketCounts())
                            Console.WriteLine($"Tag: {item.TagName} - {item.TicketCount} tickets");
                        break;

                    case "7":
                        foreach (var item in ticketService.GetTicketCountByUser())
                            Console.WriteLine($"User: {item.UserName} - {item.TicketCount} tickets");
                        break;

                    case "8":
                        Console.Write("Enter Ticket ID: ");
                        int ticketId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Tag IDs (comma separated): ");
                        var ids = Console.ReadLine().Split(',');
                        var tagIds = new List<int>();
                        foreach (var s in ids)
                            tagIds.Add(int.Parse(s.Trim()));
                        ticketService.AssignTagsToTicket(ticketId, tagIds);
                        Console.WriteLine("Tags assigned.");
                        break;

                    case "9":
                        foreach (var t in ticketService.GetTicketsWithUserAndTagNames())
                        {
                            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(t));
                        }
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
