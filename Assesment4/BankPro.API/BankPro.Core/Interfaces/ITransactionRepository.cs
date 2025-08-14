using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankPro.Core.Entities;
using BankPro.Infrastructure.Interfaces;
namespace BankPro.Core.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByAccountAsync(string accountNumber);
    }
}
