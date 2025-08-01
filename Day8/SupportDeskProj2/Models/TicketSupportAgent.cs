using SupportDeskProj2.Models;

public class TicketSupportAgent
{
    public int Id { get; set; }                    // Optional if you want a separate PK
    public int TicketId { get; set; }
    public int AgentId { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
    public virtual SupportAgent Agent { get; set; } = null!;
}
