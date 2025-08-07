using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Core.Entities
{
    public class Bug
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // e.g., Open, In Progress, Closed
        public DateTime CreatedAt { get; set; }
        public int? ProjectId { get; set; } // Nullable to allow for bugs not associated with a project
        public  Project Project { get; set; }
        public DateTime? DueDate { get; set; } 
    }

}
