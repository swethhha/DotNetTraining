using System.Collections.Generic;
using SupportDesk.Core.Entities;

namespace SupportDesk.Application.Services
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        List<Ticket> GetAllTicketsWithUsers();
        List<Ticket> GetAllTicketsWithTags();
        List<Ticket> GetAllUsersWithTickets();
        List<(string TagName, int TicketCount)> GetTagTicketCounts();
        List<(string UserName, int TicketCount)> GetTicketCountByUser();
        List<Ticket> GetTicketsByUserId(int userId);
        List<Ticket> GetTicketsByTagId(int tagId);
        List<object> GetTicketsWithUserAndTagNames();
        void AssignTagsToTicket(int ticketId, List<int> tagIds);
    }
}