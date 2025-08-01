using SupportDeskAssesment.Models;
using System;
using System.Collections.Generic;

namespace SupportDeskAssesment.Models;

public partial class TicketTag
{
    public int TicketId { get; set; }

    public int TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
