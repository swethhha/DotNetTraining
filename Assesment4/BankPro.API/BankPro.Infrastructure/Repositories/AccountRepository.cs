using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPro.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly List<Account> _accounts = new();
        private int _nextId = 1;

        public async Task AddAsync(Account entity)
        {
            await Task.Run(() =>
            {
                entity.Id = _nextId++;
                _accounts.Add(entity);
            });
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                var acc = _accounts.FirstOrDefault(a => a.Id == id);
                if (acc != null) _accounts.Remove(acc);
            });
        }

        public async Task<IEnumerable<Account>> GetAllAsync() =>
            await Task.Run(() => _accounts.AsEnumerable());

        public async Task<Account> GetByIdAsync(int id) =>
            await Task.Run(() => _accounts.FirstOrDefault(a => a.Id == id));

        public async Task UpdateAsync(Account entity)
        {
            await Task.Run(() =>
            {
                var existing = _accounts.FirstOrDefault(a => a.Id == entity.Id);
                if (existing != null)
                {
                    existing.AccountNumber = entity.AccountNumber;
                    existing.CustomerId = entity.CustomerId;
                    existing.Balance = entity.Balance;
                }
            });
        }

        // Helper to get account by account number
        public async Task<Account> GetByAccountNumberAsync(string accountNumber) =>
            await Task.Run(() => _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber));
    }
}
