using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPro.Core.DTOs
{
    public class TransactionResponseDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
