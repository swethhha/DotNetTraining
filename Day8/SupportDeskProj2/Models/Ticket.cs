using System;
using System.Collections.Generic;

namespace SupportDeskProj2.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Status { get; set; }

    public int CustomerId { get; set; }

    public int? AgentId { get; set; }

    public int? CategoryId { get; set; }

    public virtual SupportAgent? Agent { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User Customer { get; set; } = null!;

    public virtual ICollection<TicketHistory> TicketHistories { get; set; } = new List<TicketHistory>();

    public virtual ICollection<TicketSupportAgent> TicketSupportAgents { get; set; } = new List<TicketSupportAgent>();
}
