namespace HostelManagement.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public int StaffId { get; set; }
        public Staff? Staff { get; set; }
    }
}