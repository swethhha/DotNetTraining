namespace HostelManagement.MVC.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string RoomNumber { get; set; } = string.Empty;
        public string StaffName { get; set; } = string.Empty;
    }
}
