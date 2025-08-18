using BankPro.Application.Services;
using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using Moq;
using Xunit;

namespace BankPro.Test.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepoMock;
        private readonly Mock<IAccountRepository> _accountRepoMock;
        private readonly TransactionService _service;

        public TransactionServiceTests()
        {
            _transactionRepoMock = new Mock<ITransactionRepository>();
            _accountRepoMock = new Mock<IAccountRepository>();
            _service = new TransactionService(_transactionRepoMock.Object, _accountRepoMock.Object);
        }

        private Account MakeAccount(string accNo, decimal balance) =>
            new Account { AccountNumber = accNo, Balance = balance };

        [Fact]
        public async Task Deposit_Should_Increase_Balance_And_Record_Transaction()
        {
            // Arrange
            var account = MakeAccount("ACC1", 500);
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("ACC1"))
                            .ReturnsAsync(account);

            var dto = new TransactionRequestDTO
            {
                Type = "deposit",
                Amount = 200,
                ToAccount = "ACC1"
            };

            // Act
            var result = await _service.AddTransactionAsync(dto);

            // Assert
            Assert.Equal(700, account.Balance);
            Assert.Equal("deposit", result.Type, ignoreCase: true);
            _transactionRepoMock.Verify(r => r.AddAsync(It.Is<Transaction>(t =>
                t.Type == "deposit" && t.Amount == 200 && t.ToAccount == "ACC1")), Times.Once);
            _accountRepoMock.Verify(r => r.UpdateAsync(account), Times.Once);
        }

        [Fact]
        public async Task Withdraw_Should_Decrease_Balance_And_Record_Transaction()
        {
            var account = MakeAccount("ACC1", 500);
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("ACC1"))
                            .ReturnsAsync(account);

            var dto = new TransactionRequestDTO
            {
                Type = "withdraw",
                Amount = 200,
                FromAccount = "ACC1"
            };

            var result = await _service.AddTransactionAsync(dto);

            Assert.Equal(300, account.Balance);
            _transactionRepoMock.Verify(r => r.AddAsync(It.Is<Transaction>(t =>
                t.Type == "withdraw" && t.Amount == 200 && t.FromAccount == "ACC1")), Times.Once);
            _accountRepoMock.Verify(r => r.UpdateAsync(account), Times.Once);
        }

        [Fact]
        public async Task Withdraw_Should_Throw_When_InsufficientFunds()
        {
            var account = MakeAccount("ACC1", 100);
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("ACC1"))
                            .ReturnsAsync(account);

            var dto = new TransactionRequestDTO
            {
                Type = "withdraw",
                Amount = 200,
                FromAccount = "ACC1"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddTransactionAsync(dto));
            _transactionRepoMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Never);
        }

        [Fact]
        public async Task Transfer_Should_Move_Funds_And_Record_Transaction()
        {
            var from = MakeAccount("FROM", 1000);
            var to = MakeAccount("TO", 500);

            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("FROM")).ReturnsAsync(from);
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("TO")).ReturnsAsync(to);

            var dto = new TransactionRequestDTO
            {
                Type = "transfer",
                Amount = 300,
                FromAccount = "FROM",
                ToAccount = "TO"
            };

            var result = await _service.AddTransactionAsync(dto);

            Assert.Equal(700, from.Balance);
            Assert.Equal(800, to.Balance);
            _transactionRepoMock.Verify(r => r.AddAsync(It.Is<Transaction>(t =>
                t.Type == "transfer" && t.Amount == 300 &&
                t.FromAccount == "FROM" && t.ToAccount == "TO")), Times.Once);
            _accountRepoMock.Verify(r => r.UpdateAsync(from), Times.Once);
            _accountRepoMock.Verify(r => r.UpdateAsync(to), Times.Once);
        }

        [Fact]
        public async Task Transfer_Should_Throw_When_InsufficientFunds()
        {
            var from = MakeAccount("FROM", 100);
            var to = MakeAccount("TO", 500);

            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("FROM")).ReturnsAsync(from);
            _accountRepoMock.Setup(r => r.GetByAccountNumberAsync("TO")).ReturnsAsync(to);

            var dto = new TransactionRequestDTO
            {
                Type = "transfer",
                Amount = 300,
                FromAccount = "FROM",
                ToAccount = "TO"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddTransactionAsync(dto));
            _transactionRepoMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Never);
        }

        [Fact]
        public async Task GetAllTransactions_Should_Return_List()
        {
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, Amount = 100, Type = "deposit" },
                new Transaction { Id = 2, Amount = 50, Type = "withdraw" }
            };
            _transactionRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(transactions);

            var result = await _service.GetAllTransactionsAsync();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, t => t.Type == "deposit");
        }

        [Fact]
        public async Task GetTransactionById_Should_Return_Transaction()
        {
            var transaction = new Transaction { Id = 10, Amount = 500, Type = "deposit" };
            _transactionRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(transaction);

            var result = await _service.GetTransactionByIdAsync(10);

            Assert.Equal(500, result.Amount);
            Assert.Equal("deposit", result.Type, ignoreCase: true);
        }

        [Fact]
        public async Task UpdateTransaction_Should_Update_When_Found()
        {
            var transaction = new Transaction { Id = 1, Amount = 100, Type = "deposit" };
            _transactionRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(transaction);

            var dto = new TransactionRequestDTO
            {
                Amount = 200,
                Type = "withdraw",
                FromAccount = "ACC1"
            };

            var result = await _service.UpdateTransactionAsync(1, dto);

            Assert.True(result);
            _transactionRepoMock.Verify(r => r.UpdateAsync(It.Is<Transaction>(t =>
                t.Amount == 200 && t.Type == "withdraw")), Times.Once);
        }

        [Fact]
        public async Task DeleteTransaction_Should_Delete_When_Found()
        {
            var transaction = new Transaction { Id = 1, Amount = 100, Type = "deposit" };
            _transactionRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(transaction);

            var result = await _service.DeleteTransactionAsync(1);

            Assert.True(result);
            _transactionRepoMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }
}
