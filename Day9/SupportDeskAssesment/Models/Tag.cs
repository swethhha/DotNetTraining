using System;
using System.Collections.Generic;

namespace SupportDeskAssesment.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public virtual ICollection<TicketTag> TicketTags { get; set; } = new List<TicketTag>();
}
