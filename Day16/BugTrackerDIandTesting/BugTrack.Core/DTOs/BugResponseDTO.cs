﻿using BugTrack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrack.Core.DTOs
{
    public class BugResponseDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string Status { get; set; } = "Open";
        public DateTime CreatedOn { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public Project? Project { get; set; }




    }
}
