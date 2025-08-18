using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using BankPro.Application.Services;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using BankPro.Core.DTOs;
using System;

namespace BankPro.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> _accountRepoMock;
        private readonly Mock<ITransactionRepository> _transactionRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _accountRepoMock = new Mock<IAccountRepository>();
            _transactionRepoMock = new Mock<ITransactionRepository>();
            _mapperMock = new Mock<IMapper>();

            _accountService = new AccountService(
                _accountRepoMock.Object,
                _transactionRepoMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task AddAccountAsync_ShouldCallRepository()
        {
            var dto = new AccountRequestDTO { AccountNumber = "123", Balance = 100 };
            var account = new Account { Id = 1, AccountNumber = "123", Balance = 100 };

            _mapperMock.Setup(m => m.Map<Account>(dto)).Returns(account);

            await _accountService.AddAccountAsync(dto);

            _accountRepoMock.Verify(r => r.AddAsync(account), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAccount_WhenExists()
        {
            var account = new Account { Id = 1, AccountNumber = "123", Balance = 200 };
            _accountRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(account);

            var result = await _accountService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("123", result.AccountNumber);
            Assert.Equal(200, result.Balance);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAccounts()
        {
            var accounts = new List<Account>
            {
                new Account { Id = 1, AccountNumber = "123", Balance = 100 }
            };

            _accountRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(accounts);

            var result = await _accountService.GetAllAsync();

            Assert.Single(result);
        }

        [Fact]
        public async Task DeleteAccountAsync_ShouldCallRepository()
        {
            await _accountService.DeleteAccountAsync(1);

            _accountRepoMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DepositAsync_ShouldIncreaseBalance_AndLogTransaction()
        {
            var account = new Account { Id = 1, AccountNumber = "123", Balance = 100 };
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("123")).ReturnsAsync(account);

            await _accountService.DepositAsync("123", 50);

            Assert.Equal(150, account.Balance);
            _accountRepoMock.Verify(r => r.UpdateAsync(account), Times.Once);
            _transactionRepoMock.Verify(t => t.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Fact]
        public async Task WithdrawAsync_ShouldDecreaseBalance_WhenSufficient()
        {
            var account = new Account { Id = 1, AccountNumber = "123", Balance = 200 };
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("123")).ReturnsAsync(account);

            await _accountService.WithdrawAsync("123", 50);

            Assert.Equal(150, account.Balance);
            _accountRepoMock.Verify(r => r.UpdateAsync(account), Times.Once);
            _transactionRepoMock.Verify(t => t.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Fact]
        public async Task WithdrawAsync_ShouldThrow_WhenInsufficientBalance()
        {
            var account = new Account { Id = 1, AccountNumber = "123", Balance = 30 };
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("123")).ReturnsAsync(account);

            await Assert.ThrowsAsync<Exception>(() => _accountService.WithdrawAsync("123", 50));
        }

        [Fact]
        public async Task TransferAsync_ShouldMoveBalanceBetweenAccounts()
        {
            var from = new Account { Id = 1, AccountNumber = "123", Balance = 200 };
            var to = new Account { Id = 2, AccountNumber = "456", Balance = 100 };

            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("123")).ReturnsAsync(from);
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("456")).ReturnsAsync(to);

            await _accountService.TransferAsync("123", "456", 50);

            Assert.Equal(150, from.Balance);
            Assert.Equal(150, to.Balance);

            _accountRepoMock.Verify(r => r.UpdateAsync(from), Times.Once);
            _accountRepoMock.Verify(r => r.UpdateAsync(to), Times.Once);
            _transactionRepoMock.Verify(t => t.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }
    }
}
