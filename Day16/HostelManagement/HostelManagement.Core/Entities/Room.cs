namespace HostelManagement.Core.Entities
{
    public class Room
    {
        public int Id { get; set; }

        public string RoomNumber { get; set; } = string.Empty;

        // Fixed capacity
        public int Capacity { get; private set; } = 4;

        public int? StaffId { get; set; }
        public Staff? Staff { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

        // Method to set RoomNumber based on Id
        public void GenerateRoomNumber()
        {
            RoomNumber = $"R{Id}";
        }
    }
}
