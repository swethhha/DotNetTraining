using System.ComponentModel.DataAnnotations;

namespace HostelManagement.Core.DTOs
{
    public class StudentRequestDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        // client can request a preferred room; service will validate capacity
        public int? PreferredRoomId { get; set; }
    }
}
