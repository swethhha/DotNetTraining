using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelManagement.Core.DTOs
{
    public class RoomRequestDTO
    {
        [Required]
        public string RoomNumber { get; set; } = string.Empty;
        public int? StaffId { get; set; }
    }
}
