using AutoMapper;
using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankPro.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepo, ITransactionRepository transactionRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
            _mapper = mapper;
        }

        public async Task AddAccountAsync(AccountRequestDTO dto)
        {
            var account = _mapper.Map<Account>(dto);
            await _accountRepo.AddAsync(account);
        }

        public async Task DeleteAccountAsync(int id)
        {
            await _accountRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _accountRepo.GetAllAsync();
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _accountRepo.GetByIdAsync(id);
        }

        public async Task UpdateAccountAsync(int id, AccountRequestDTO dto)
        {
            var existing = await _accountRepo.GetByIdAsync(id);
            if (existing != null)
            {
                _mapper.Map(dto, existing);
                await _accountRepo.UpdateAsync(existing);
            }
        }

        // Deposit money
        public async Task DepositAsync(string accountNumber, decimal amount)
        {
            var account = await _accountRepo.GetByAccountNumberAsync(accountNumber);
            if (account == null) throw new Exception("Account not found");

            account.Balance += amount;
            await _accountRepo.UpdateAsync(account);

            // Log transaction
            await _transactionRepo.AddAsync(new Transaction
            {
                Type = "Deposit",
                ToAccount = accountNumber,
                FromAccount = null,
                Amount = amount,
                Date = DateTime.Now
            });
        }

        // Withdraw money
        public async Task WithdrawAsync(string accountNumber, decimal amount)
        {
            var account = await _accountRepo.GetByAccountNumberAsync(accountNumber);
            if (account == null) throw new Exception("Account not found");
            if (account.Balance < amount) throw new Exception("Insufficient balance");

            account.Balance -= amount;
            await _accountRepo.UpdateAsync(account);

            // Log transaction
            await _transactionRepo.AddAsync(new Transaction
            {
                Type = "Withdraw",
                FromAccount = accountNumber,
                ToAccount = null,
                Amount = amount,
                Date = DateTime.Now
            });
        }

        // Transfer money
        public async Task TransferAsync(string fromAccountNumber, string toAccountNumber, decimal amount)
        {
            var fromAccount = await _accountRepo.GetByAccountNumberAsync(fromAccountNumber);
            var toAccount = await _accountRepo.GetByAccountNumberAsync(toAccountNumber);

            if (fromAccount == null || toAccount == null) throw new Exception("Account not found");
            if (fromAccount.Balance < amount) throw new Exception("Insufficient balance");

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            await _accountRepo.UpdateAsync(fromAccount);
            await _accountRepo.UpdateAsync(toAccount);

            // Log transaction
            await _transactionRepo.AddAsync(new Transaction
            {
                Type = "Transfer",
                FromAccount = fromAccountNumber,
                ToAccount = toAccountNumber,
                Amount = amount,
                Date = DateTime.Now
            });
        }
    }
}
