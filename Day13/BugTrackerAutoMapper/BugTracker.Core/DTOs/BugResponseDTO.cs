using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Core.DTOs
{
    public class BugResponseDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public required string Status { get; set; } // e.g., Open, In Progress, Closed

        public DateTime? DueDate { get; set; } // Nullable to allow for no due date
    }
}
