using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPro.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }           
        public required string AccountNumber { get; set; }
        public int CustomerId { get; set; } 
        public decimal Balance { get; set; }
    }
}
