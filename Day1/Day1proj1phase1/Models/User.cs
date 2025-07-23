namespace Day1proj1phase1.Models
{
    public class User
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Roll { get; set; } // Use "Admin" or "User"

        public User(int id, string name, string roll)
        {
            Id = id;
            Name = name;
            Roll = roll;
        }

        public void DisplayUser()
        {
            Console.WriteLine($"Id: {Id}, Name: {Name}, Roll: {Roll}");
        }
    }
}
