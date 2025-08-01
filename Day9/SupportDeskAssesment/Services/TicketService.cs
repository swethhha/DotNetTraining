using SupportDeskAssesment.Data;
using SupportDeskAssesment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class TicketService : ITicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    public void CreateTicket(string title, string description, int userId, List<int> tagIds)
    {
        var ticket = new Ticket
        {
            Title = title,
            Description = description,
            UserId = userId,
            Status = "Open",
            TicketTags = tagIds.Select(id => new TicketTag { TagId = id }).ToList()
        };

        _context.Tickets.Add(ticket);
        _context.SaveChanges();
    }

    public List<Ticket> GetAllTickets()
    {
        return _context.Tickets
            .Include(t => t.User)
            .Include(t => t.TicketTags).ThenInclude(tt => tt.Tag)
            .ToList();
    }
}
