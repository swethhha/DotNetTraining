namespace EventEase.MVC.Models
{
    public class RegistrationViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public DateTime RegisteredOn { get; set; }

        public string? UserName { get; set; }
        public string? EventTitle { get; set; }
    }
}
