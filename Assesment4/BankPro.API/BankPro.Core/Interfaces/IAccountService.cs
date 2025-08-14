using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankPro.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account> GetByIdAsync(int id);
        Task AddAccountAsync(AccountRequestDTO dto);
        Task UpdateAccountAsync(int id, AccountRequestDTO dto);
        Task DeleteAccountAsync(int id);
        Task DepositAsync(string accountNumber, decimal amount);
        Task WithdrawAsync(string accountNumber, decimal amount);
        Task TransferAsync(string fromAccount, string toAccount, decimal amount);
      
    }
}
