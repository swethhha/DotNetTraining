namespace HostelManagement.Core.DTOs
{
    public class StaffResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }

        // Dynamically calculated
        public int RoomsManaged { get; set; }
        public List<string> RoomNumbers { get; set; } = new();
    }
}
