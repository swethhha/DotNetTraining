namespace HostelManagement.Core.DTOs
{
    public class StaffRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; } = 5; // Max rooms staff can manage
    }
}
