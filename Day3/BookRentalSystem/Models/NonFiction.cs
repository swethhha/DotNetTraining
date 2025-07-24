namespace BookRentalSystem.Models
{
    public class NonFiction : Book, IRentable
    {
        public string Category { get; set; } = "";

        public void Rent()
        {
            IsAvailable = false;
        }

        public void Return()
        {
            IsAvailable = true;
        }

        public void ReportStatus()
        {
            Console.WriteLine($"NonFiction #{Id}: {(IsAvailable ? "Available" : "Rented")} | Category: {Category}");
        }

        public override void Display()
        {
            Console.WriteLine($"[Non-Fiction] \"{Title}\" by {Author} | Category: {Category} | Status: {(IsAvailable ? "Available" : "Rented")}");
        }
    }
}
