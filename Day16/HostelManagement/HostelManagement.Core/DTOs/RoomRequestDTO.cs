namespace HostelManagement.Core.DTOs
{
    public class RoomRequestDTO
    {
        public string RoomNumber { get; set; } = string.Empty;
        public int Capacity { get; set; } = 4; // Default capacity
    }
}
