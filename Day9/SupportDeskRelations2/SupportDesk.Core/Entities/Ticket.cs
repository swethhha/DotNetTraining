namespace SupportDesk.Core.Entities;

public class Ticket
{
    public int TicketId { get; set; }
    public string Title { get; set; } = null!;
    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public virtual ICollection<TicketTag> TicketTags { get; set; } = new List<TicketTag>();
}

