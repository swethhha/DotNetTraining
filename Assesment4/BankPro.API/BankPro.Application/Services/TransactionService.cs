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
        private readonly IAccountRepository _accountRepository;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
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

        public async Task<TransactionResponseDTO> AddTransactionAsync(TransactionRequestDTO dto)
        {
            // Deposit
            if (dto.Type.Equals("deposit", StringComparison.OrdinalIgnoreCase))
            {
                var account = await _accountRepository.GetByAccountNumberAsync(dto.ToAccount);
                if (account == null)
                    throw new KeyNotFoundException("Destination account not found.");

                account.Balance += dto.Amount;
                await _accountRepository.UpdateAsync(account);
            }
            // Withdraw
            else if (dto.Type.Equals("withdraw", StringComparison.OrdinalIgnoreCase))
            {
                var account = await _accountRepository.GetByAccountNumberAsync(dto.FromAccount);
                if (account == null)
                    throw new KeyNotFoundException("Source account not found.");
                if (account.Balance < dto.Amount)
                    throw new InvalidOperationException("Insufficient funds.");

                account.Balance -= dto.Amount;
                await _accountRepository.UpdateAsync(account);
            }
            // Transfer
            else if (dto.Type.Equals("transfer", StringComparison.OrdinalIgnoreCase))
            {
                var fromAccount = await _accountRepository.GetByAccountNumberAsync(dto.FromAccount);
                var toAccount = await _accountRepository.GetByAccountNumberAsync(dto.ToAccount);

                if (fromAccount == null || toAccount == null)
                    throw new KeyNotFoundException("One or both accounts not found.");
                if (fromAccount.Balance < dto.Amount)
                    throw new InvalidOperationException("Insufficient funds.");

                fromAccount.Balance -= dto.Amount;
                toAccount.Balance += dto.Amount;

                await _accountRepository.UpdateAsync(fromAccount);
                await _accountRepository.UpdateAsync(toAccount);
            }
            else
            {
                throw new InvalidOperationException("Invalid transaction type.");
            }

            // Save transaction in repository
            var transaction = new Transaction
            {
                Amount = dto.Amount,
                Type = dto.Type,
                FromAccount = dto.FromAccount,
                ToAccount = dto.ToAccount,
                Date = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transaction);

            return new TransactionResponseDTO
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Type = transaction.Type,
                FromAccount = transaction.FromAccount,
                ToAccount = transaction.ToAccount,
                Date = transaction.Date
            };
        }

        public async Task<bool> UpdateTransactionAsync(int id, TransactionRequestDTO dto)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null) return false;

            transaction.Amount = dto.Amount;
            transaction.Type = dto.Type;
            transaction.FromAccount = dto.FromAccount;
            transaction.ToAccount = dto.ToAccount;
            transaction.Date = DateTime.UtcNow;

            await _transactionRepository.UpdateAsync(transaction);
            return true;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null) return false;

            await _transactionRepository.DeleteAsync(id);
            return true;
        }
    }
}
