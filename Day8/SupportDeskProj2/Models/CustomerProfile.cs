using System;
using System.Collections.Generic;

namespace SupportDeskProj2.Models;

public partial class CustomerProfile
{
    public int ProfileId { get; set; }

    public int UserId { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual User User { get; set; } = null!;
}
