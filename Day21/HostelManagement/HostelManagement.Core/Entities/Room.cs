namespace HostelManagement.Core.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int Capacity { get; set; } = 4;
        public int StaffId { get; set; }
        public Staff? Staff { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}