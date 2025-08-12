using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelManagement.Core.DTOs
{
    public class RoomResponseDTO
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int Capacity { get; } = 4; // always fixed
        public int? StaffId { get; set; }
        public string? StaffName { get; set; }
        public List<StudentResponseDTO> Students { get; set; } = new();
    }

}
