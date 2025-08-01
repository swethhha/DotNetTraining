using System;
using System.Collections.Generic;

namespace SupportDeskProj2.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string Role { get; set; } = null!;

    public virtual CustomerProfile? CustomerProfile { get; set; }

    public virtual SupportAgent? SupportAgent { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
