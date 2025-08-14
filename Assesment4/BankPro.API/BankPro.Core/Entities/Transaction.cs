using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Transaction
{
    public int Id { get; set; }
    public string Type { get; set; }     // "Deposit", "Withdrawal", "Transfer"
    public string FromAccount { get; set; }  // For deposit, can be null
    public string ToAccount { get; set; }    // For withdrawal, can be null
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}
