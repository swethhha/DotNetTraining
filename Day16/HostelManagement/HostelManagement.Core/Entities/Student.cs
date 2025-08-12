using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelManagement.Core.Entities
{ 
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // nullable - unassigned student has RoomId == null
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
    }
}

