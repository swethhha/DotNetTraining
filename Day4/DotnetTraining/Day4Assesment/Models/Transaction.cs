using System;

namespace Day4Assesment.Models
{
    public class Transaction
    {
        public string Type { get; set; }
        public double Amt { get; set; }
        public DateTime Time { get; set; }

        public Transaction(string type, double amt)
        {
            Type = type;
            Amt = amt;
            Time = DateTime.Now;
        }

        public void Print()
        {
            Console.WriteLine($"{Time:dd-MM-yyyy hh:mm tt} - {Type} of Rs.{Amt}");
        }
    }
}
