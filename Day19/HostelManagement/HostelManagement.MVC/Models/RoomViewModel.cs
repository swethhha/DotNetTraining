namespace HostelManagement.MVC.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int CurrentOccupancy { get; set; }
        public List<string> Students { get; set; } = new();
    }
}
