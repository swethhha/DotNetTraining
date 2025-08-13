namespace HostelManagement.Core.DTOs
{
    public class RoomResponseDTO
    {
        public int Id { get; set; }
        public required string RoomNumber { get; set; } 
        public int Capacity { get; set; }
        public int CurrentOccupancy => Students.Count;
        public List<string> Students { get; set; } = new();
    }
}
