using SupportDeskAssesment.Models;
using System.Collections.Generic;

public interface ITicketService
{
    void CreateTicket(string title, string description, int userId, List<int> tagIds);
    List<Ticket> GetAllTickets();
}
