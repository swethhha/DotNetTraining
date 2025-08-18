using System.Collections.Generic;

namespace SupportDesk.Core.Entities
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
