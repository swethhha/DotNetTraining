using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPro.Core.Entities
{
    public class Customer
    {
        public int Id { get; set; }          
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
