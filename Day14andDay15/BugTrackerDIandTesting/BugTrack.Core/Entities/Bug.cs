﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrack.Core.Entities
{
    public class Bug
    {
        public int Id { get; set; } 
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string Status { get; set; } = "Open"; // Default status is "Open"
        public DateTime CreatedOn { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
