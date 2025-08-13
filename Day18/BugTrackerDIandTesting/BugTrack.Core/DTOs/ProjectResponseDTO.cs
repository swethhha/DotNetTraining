using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrack.Core.DTOs
{
    public class ProjectResponseDTO
    {
        public int ProjectId { get; set; }
        public required string ProjectName { get; set; }
        public required string Description { get; set; }

    }
}
