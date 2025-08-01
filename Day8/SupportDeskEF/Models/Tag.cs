using System;
using System.Collections.Generic;

namespace SupportDeskEF.Models;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public virtual ICollection<TicketTag> TicketTags { get; set; } = new List<TicketTag>();
}
