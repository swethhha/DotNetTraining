using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPro.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly List<Transaction> _transactions = new();
        private int _nextId = 1;

        public async Task AddAsync(Transaction entity)
        {
            await Task.Run(() =>
            {
                entity.Id = _nextId++;
                _transactions.Add(entity);
            });
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                var tran = _transactions.FirstOrDefault(t => t.Id == id);
                if (tran != null) _transactions.Remove(tran);
            });
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync() =>
            await Task.Run(() => _transactions.AsEnumerable());

        public async Task<Transaction> GetByIdAsync(int id) =>
            await Task.Run(() => _transactions.FirstOrDefault(t => t.Id == id));

        public async Task UpdateAsync(Transaction entity)
        {
            await Task.Run(() =>
            {
                var existing = _transactions.FirstOrDefault(t => t.Id == entity.Id);
                if (existing != null)
                {
                    existing.Type = entity.Type;
                    existing.FromAccount = entity.FromAccount;
                    existing.ToAccount = entity.ToAccount;
                    existing.Amount = entity.Amount;
                    existing.Date = entity.Date;
                }
            });
        }

        // Helper: get transactions by account
        public async Task<IEnumerable<Transaction>> GetByAccountAsync(string accountNumber) =>
            await Task.Run(() => _transactions
                .Where(t => t.FromAccount == accountNumber || t.ToAccount == accountNumber));
    }
}
