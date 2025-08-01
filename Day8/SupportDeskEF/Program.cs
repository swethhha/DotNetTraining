using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SupportDeskEF.Models;

namespace SupportDeskEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new AppDbContext();

            context.Database.EnsureCreated();

            // Ensure user "Alice" exists or create
            var user = context.Users.FirstOrDefault(u => u.UserName == "Alice");
            if (user == null)
            {
                user = new User { UserName = "Alice" };
                context.Users.Add(user);
                context.SaveChanges();
            }

            // Ensure tags "Bug" and "UI" exist or create
            var tag1 = context.Tags.FirstOrDefault(t => t.Name == "Bug");
            if (tag1 == null)
            {
                tag1 = new Tag { Name = "Bug" };
                context.Tags.Add(tag1);
            }

            var tag2 = context.Tags.FirstOrDefault(t => t.Name == "UI");
            if (tag2 == null)
            {
                tag2 = new Tag { Name = "UI" };
                context.Tags.Add(tag2);
            }

            context.SaveChanges();

            // Delete duplicate tickets with same title and user (keep one)
            var duplicateTickets = context.Tickets
                .Include(t => t.User)
                .Where(t => t.Title == "Login issue" && t.UserId == user.UserId)
                .OrderBy(t => t.TicketId)
                .ToList();

            if (duplicateTickets.Count > 1)
            {
                for (int i = 1; i < duplicateTickets.Count; i++) // Skip first one
                {
                    var ticketId = duplicateTickets[i].TicketId;

                    // Remove related TicketTags
                    var tagsToRemove = context.TicketTags.Where(tt => tt.TicketId == ticketId);
                    context.TicketTags.RemoveRange(tagsToRemove);

                    // Remove the ticket
                    context.Tickets.Remove(duplicateTickets[i]);
                }
                context.SaveChanges();
            }

            // Check again if ticket already exists
            var existingTicket = context.Tickets
                .FirstOrDefault(t => t.Title == "Login issue" && t.UserId == user.UserId);

            if (existingTicket == null)
            {
                var ticket = new Ticket
                {
                    Title = "Login issue",
                    UserId = user.UserId
                };
                context.Tickets.Add(ticket);
                context.SaveChanges();

                // Link to tags
                context.TicketTags.Add(new TicketTag { TicketId = ticket.TicketId, TagId = tag1.Id });
                context.TicketTags.Add(new TicketTag { TicketId = ticket.TicketId, TagId = tag2.Id });
                context.SaveChanges();
            }

            // Retrieve and print tickets
            var tickets = context.Tickets
                .Include(t => t.User)
                .Include(t => t.TicketTags)
                    .ThenInclude(tt => tt.Tag)
                .ToList();

            foreach (var ticket in tickets)
            {
                Console.WriteLine("Title: " + ticket.Title);
                Console.WriteLine("Raised by: " + ticket.User?.UserName);
                Console.Write("Tags: ");
                foreach (var tt in ticket.TicketTags)
                {
                    Console.Write(tt.Tag?.Name + " ");
                }
                Console.WriteLine();
                Console.WriteLine("--------------------------");
            }

            Console.ReadKey();
        }
    }
}
