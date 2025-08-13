namespace HostelManagement.Core.Entities
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Capacity { get; set; } = 5;
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}