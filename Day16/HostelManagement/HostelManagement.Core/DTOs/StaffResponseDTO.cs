using System.Collections.Generic;

namespace HostelManagement.Core.DTOs
{
    public class StaffResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Max rooms = 5, but just return whatever is assigned
        public List<RoomResponseDTO> Rooms { get; set; } = new();
    }
}
