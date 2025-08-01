using Microsoft.EntityFrameworkCore;
using SupportDeskProj2.Models;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using var context = new SupportDeskProj2Context();

        // RESET DB
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Seed Department
        var dept = new Department { Name = "IT Support" };
        context.Departments.Add(dept);

        // Seed Users
        var user1 = new User { UserName = "Alice", Email = "alice@example.com", Role = "Customer" };
        var user2 = new User { UserName = "Bob", Email = "bob@example.com", Role = "SupportAgent" };
        context.Users.AddRange(user1, user2);
        context.SaveChanges();

        // Customer Profile
        var profile = new CustomerProfile { UserId = user1.UserId, Address = "123 Street", Phone = "1234567890" };
        context.CustomerProfiles.Add(profile);

        // Agent Profile
        var agent = new SupportAgent { UserId = user2.UserId, DepartmentId = dept.DepartmentId };
        context.SupportAgents.Add(agent);

        // Category
        var category = new Category { Name = "Login Issues" };
        context.Categories.Add(category);
        context.SaveChanges();

        // CREATE Ticket
        var ticket = new Ticket
        {
            Title = "Cannot login",
            Description = "Forgot password",
            Status = "Open",
            CustomerId = user1.UserId,
            AgentId = agent.AgentId,
            CategoryId = category.CategoryId
        };
        context.Tickets.Add(ticket);
        context.SaveChanges();

        AddHistory(context, ticket.TicketId, "Open");

        // UPDATE Ticket
        ticket.Status = "In Progress";
        context.Tickets.Update(ticket);
        context.SaveChanges();

        AddHistory(context, ticket.TicketId, "In Progress");

        // DELETE Ticket
        context.Tickets.Remove(ticket);
        context.SaveChanges();

        AddHistory(context, ticket.TicketId, "Deleted");

        // READ
        var allTickets = context.Tickets
            .Include(t => t.Customer)
            .Include(t => t.Agent).ThenInclude(a => a.User)
            .Include(t => t.Category)
            .ToList();

        foreach (var t in allTickets)
        {
            Console.WriteLine($"Ticket: {t.Title}, Status: {t.Status}");
            Console.WriteLine($"Customer: {t.Customer.UserName}");
            Console.WriteLine($"Assigned Agent: {t.Agent?.User?.UserName ?? "Unassigned"}");
            Console.WriteLine($"Category: {t.Category?.Name}");
            Console.WriteLine("-----");
        }

        // Read history
        var histories = context.TicketHistories.ToList();
        Console.WriteLine("\nTicket History:");
        foreach (var h in histories)
        {
            Console.WriteLine($"TicketId: {h.TicketId}, Status: {h.Status}, Date: {h.UpdatedDate}");
        }
    }

    static void AddHistory(SupportDeskProj2Context context, int ticketId, string status)
    {
        var history = new TicketHistory
        {
            TicketId = ticketId,
            Status = status,
            UpdatedDate = DateTime.Now
        };
        context.TicketHistories.Add(history);
        context.SaveChanges();
    }
}
