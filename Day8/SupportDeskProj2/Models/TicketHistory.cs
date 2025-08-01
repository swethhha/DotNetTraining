using System;
using System.Collections.Generic;

namespace SupportDeskProj2.Models;

public partial class TicketHistory
{
    public int HistoryId { get; set; }

    public int TicketId { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
}
