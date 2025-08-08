using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrack.Core.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public required string ProjectName { get; set; }

        public ICollection<Bug> Bugs { get; set; } = new List<Bug>();
    }
}
