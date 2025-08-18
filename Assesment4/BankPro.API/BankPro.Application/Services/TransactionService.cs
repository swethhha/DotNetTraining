using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPro.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<TransactionResponseDTO>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Select(t => new TransactionResponseDTO
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type,
                FromAccount = t.FromAccount,
                ToAccount = t.ToAccount,
                Date = t.Date
            });
        }

        public async Task<TransactionResponseDTO> GetTransactionByIdAsync(int id)
        {
            var t = await _transactionRepository.GetByIdAsync(id);
            if (t == null) return null;

            return new TransactionResponseDTO
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type,
                FromAccount = t.FromAccount,
                ToAccount = t.ToAccount,
                Date = t.Date
            };
        }

        public async Task<IEnumerable<TransactionResponseDTO>> GetTransactionsByTypeAsync(string type)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions
                .Where(t => t.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                .Select(t => new TransactionResponseDTO
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Type = t.Type,
                    FromAccount = t.FromAccount,
                    ToAccount = t.ToAccount,
                    Date = t.Date
                });
        }

        public async Task<IEnumerable<TransactionResponseDTO>> GetTransactionsByDateRangeAsync(DateTime from, DateTime to)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions
                .Where(t => t.Date.Date >= from.Date && t.Date.Date <= to.Date)
                .Select(t => new TransactionResponseDTO
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Type = t.Type,
                    FromAccount = t.FromAccount,
                    ToAccount = t.ToAccount,
                    Date = t.Date
                });
        }
    }
}
