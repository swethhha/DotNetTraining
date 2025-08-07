using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Core.DTOs
{
    public class BugRequestDTO
    {
        public int Id { get; set; }

      
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; } // e.g., Open, In Progress, Closed
        public DateTime? DueDate { get; set; }

    }
}
