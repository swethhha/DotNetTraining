using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPro.Core.DTOs
{
    public class TransactionRequestDTO
    {
        public string Type { get; set; }          // "Deposit", "Withdraw", "Transfer"
        public string FromAccount { get; set; }   // null for deposit
        public string ToAccount { get; set; }     // null for withdraw
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now; // default to current time
    }
}
