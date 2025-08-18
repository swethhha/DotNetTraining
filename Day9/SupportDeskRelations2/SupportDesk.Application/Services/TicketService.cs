using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SupportDesk.Core.Entities;
using SupportDesk.Infrastructure;
using SupportDesk.Application.Services;

namespace SupportDesk.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly SupportDeskDbContext _context;

        public TicketService(SupportDeskDbContext context)
        {
            _context = context;
        }

        public List<Ticket> GetAllTickets() =>
            _context.Tickets.ToList();

        public List<Ticket> GetAllTicketsWithUsers() =>
            _context.Tickets.Include(t => t.User).ToList();

        public List<Ticket> GetAllTicketsWithTags() =>
            _context.Tickets.Include(t => t.Tags).ToList();

        public List<Ticket> GetAllUsersWithTickets() =>
            _context.Tickets.Include(t => t.User).ToList();

        public List<(string TagName, int TicketCount)> GetTagTicketCounts() =>
            _context.Tags
                .Select(tag => new
                {
                    TagName = tag.Name,
                    TicketCount = tag.Tickets.Count
                })
                .ToList()
                .Select(x => (x.TagName, x.TicketCount))
                .ToList();

        public List<(string UserName, int TicketCount)> GetTicketCountByUser() =>
            _context.Users
                .Select(u => new
                {
                    UserName = u.Name,
                    TicketCount = u.Tickets.Count
                })
                .ToList()
                .Select(x => (x.UserName, x.TicketCount))
                .ToList();

        public List<Ticket> GetTicketsByUserId(int userId) =>
            _context.Tickets.Where(t => t.UserId == userId).ToList();

        public List<Ticket> GetTicketsByTagId(int tagId) =>
            _context.Tickets
                .Where(t => t.Tags.Any(tag => tag.Id == tagId))
                .ToList();

        public List<object> GetTicketsWithUserAndTagNames() =>
            _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Tags)
                .Select(t => new
                {
                    TicketId = t.Id,
                    Title = t.Title,
                    AssignedTo = t.User != null ? t.User.Name : "Unassigned",
                    Tags = t.Tags.Select(tag => tag.Name).ToList()
                })
                .Cast<object>()
                .ToList();

        public void AssignTagsToTicket(int ticketId, List<int> tagIds)
        {
            var ticket = _context.Tickets
                .Include(t => t.Tags)
                .FirstOrDefault(t => t.Id == ticketId);

            if (ticket == null)
                return;

            var tags = _context.Tags.Where(t => tagIds.Contains(t.Id)).ToList();
            ticket.Tags = tags;
            _context.SaveChanges();
        }
    }
}
