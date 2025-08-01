
using System;
using System.Collections.Generic;

namespace SupportDeskAssesment.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public virtual ICollection<TicketTag> TicketTags { get; set; } = new List<TicketTag>();
}
