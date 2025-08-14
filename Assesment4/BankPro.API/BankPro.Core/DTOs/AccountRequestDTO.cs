using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPro.Core.DTOs
{
    public class AccountRequestDTO
    {
        public required string AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
}
