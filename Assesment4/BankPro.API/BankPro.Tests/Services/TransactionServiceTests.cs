using BankPro.Application.Services;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using BankPro.Core.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BankPro.Tests.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<IAccountRepository> _accountRepoMock;
        private readonly Mock<ITransactionRepository> _transactionRepoMock;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            _accountRepoMock = new Mock<IAccountRepository>();
            _transactionRepoMock = new Mock<ITransactionRepository>();
            _transactionService = new TransactionService(
                _transactionRepoMock.Object,
                _accountRepoMock.Object
            );
        }

        private Account GetAccount(int id, string accNumber, decimal balance) =>
            new Account { Id = id, AccountNumber = accNumber, Balance = balance };

        [Fact]
        public async Task Deposit_ShouldIncreaseBalance_AndSaveTransaction()
        {
            // Arrange
            var account = GetAccount(1, "ACC001", 500);
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("ACC001")).ReturnsAsync(account);
            _accountRepoMock.Setup(r => r.UpdateAsync(account)).Returns(Task.CompletedTask);
            _transactionRepoMock.Setup(r => r.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

            var dto = new TransactionRequestDTO
            {
                Type = "deposit",
                ToAccount = "ACC001",
                Amount = 200
            };

            // Act
            var result = await _transactionService.AddTransactionAsync(dto);

            // Assert
            Assert.Equal(700, account.Balance);
            Assert.Equal("deposit", result.Type);
            _accountRepoMock.Verify(r => r.UpdateAsync(account), Times.Once);
            _transactionRepoMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Fact]
        public async Task Withdraw_ShouldDecreaseBalance_AndSaveTransaction()
        {
            // Arrange
            var account = GetAccount(1, "ACC001", 500);
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("ACC001")).ReturnsAsync(account);
            _accountRepoMock.Setup(r => r.UpdateAsync(account)).Returns(Task.CompletedTask);
            _transactionRepoMock.Setup(r => r.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

            var dto = new TransactionRequestDTO
            {
                Type = "withdraw",
                FromAccount = "ACC001",
                Amount = 200
            };

            // Act
            var result = await _transactionService.AddTransactionAsync(dto);

            // Assert
            Assert.Equal(300, account.Balance);
            Assert.Equal("withdraw", result.Type);
            _accountRepoMock.Verify(r => r.UpdateAsync(account), Times.Once);
            _transactionRepoMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Fact]
        public async Task Transfer_ShouldMoveFunds_AndSaveTransaction()
        {
            // Arrange
            var source = GetAccount(1, "ACC001", 1000);
            var destination = GetAccount(2, "ACC002", 500);

            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("ACC001")).ReturnsAsync(source);
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("ACC002")).ReturnsAsync(destination);
            _accountRepoMock.Setup(r => r.UpdateAsync(source)).Returns(Task.CompletedTask);
            _accountRepoMock.Setup(r => r.UpdateAsync(destination)).Returns(Task.CompletedTask);
            _transactionRepoMock.Setup(r => r.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

            var dto = new TransactionRequestDTO
            {
                Type = "transfer",
                FromAccount = "ACC001",
                ToAccount = "ACC002",
                Amount = 300
            };

            // Act
            var result = await _transactionService.AddTransactionAsync(dto);

            // Assert
            Assert.Equal(700, source.Balance);
            Assert.Equal(800, destination.Balance);
            Assert.Equal("transfer", result.Type);
            _accountRepoMock.Verify(r => r.UpdateAsync(source), Times.Once);
            _accountRepoMock.Verify(r => r.UpdateAsync(destination), Times.Once);
            _transactionRepoMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Fact]
        public async Task Transfer_ShouldFail_WhenInsufficientBalance()
        {
            // Arrange
            var source = GetAccount(1, "ACC001", 100);
            var destination = GetAccount(2, "ACC002", 500);

            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("ACC001")).ReturnsAsync(source);
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("ACC002")).ReturnsAsync(destination);

            var dto = new TransactionRequestDTO
            {
                Type = "transfer",
                FromAccount = "ACC001",
                ToAccount = "ACC002",
                Amount = 200
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _transactionService.AddTransactionAsync(dto)
            );

            _transactionRepoMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Never);
        }
    }
}
