namespace BookRentalSystem.Models
{
    public class Fiction : Book, IRentable
    {
        public string Genre { get; set; } = "";

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
            Console.WriteLine($"Fiction #{Id}: {(IsAvailable ? "Available" : "Rented")} | Genre: {Genre}");
        }

        public override void Display()
        {
            Console.WriteLine($"[Fiction] \"{Title}\" by {Author} | Genre: {Genre} | Status: {(IsAvailable ? "Available" : "Rented")}");
        }
    }
}
