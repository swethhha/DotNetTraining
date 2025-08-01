using SupportDeskEF.Models;
public class TicketTag
{
    public int TicketId { get; set; }
    public int TagId { get; set; }

    public Ticket Ticket { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}

