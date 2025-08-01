using System;
using System.Collections.Generic;

namespace SupportDeskProj2.Models;

public partial class SupportAgent
{
    public int AgentId { get; set; }

    public int UserId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<TicketSupportAgent> TicketSupportAgents { get; set; } = new List<TicketSupportAgent>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual User User { get; set; } = null!;
}
