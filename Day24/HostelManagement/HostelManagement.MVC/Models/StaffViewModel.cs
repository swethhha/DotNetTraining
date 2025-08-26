namespace HostelManagement.MVC.Models
{
    public class StaffViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int RoomsManaged { get; set; }
        public List<string> RoomNumbers { get; set; } = new();
    }
}
