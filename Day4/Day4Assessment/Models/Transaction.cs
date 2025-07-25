using System;

namespace Day4Assessment.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty;
        public double Amount { get; set; }

        public override string ToString()
        {
            return $"{Date:g} - {Type} - ₹{Amount}";
        }
    }
}
