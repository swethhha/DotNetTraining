using BankPro.Application.Services;
using BankPro.Core.DTOs;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BankPro.Tests.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepoMock;
        private readonly TransactionService _service;

        public TransactionServiceTests()
        {
            _transactionRepoMock = new Mock<ITransactionRepository>();
            _service = new TransactionService(_transactionRepoMock.Object);
        }

        private List<Transaction> GetSampleTransactions()
        {
            return new List<Transaction>
            {
                new Transaction { Id = 1, Type = "deposit", FromAccount = null, ToAccount = "101", Amount = 100, Date = new DateTime(2025, 8, 15) },
                new Transaction { Id = 2, Type = "withdraw", FromAccount = "101", ToAccount = null, Amount = 50, Date = new DateTime(2025, 8, 16) },
                new Transaction { Id = 3, Type = "transfer", FromAccount = "101", ToAccount = "102", Amount = 30, Date = new DateTime(2025, 8, 17) },
            };
        }

        [Fact]
        public async Task GetAllTransactionsAsync_ShouldReturnAllTransactions()
        {
            // Arrange
            _transactionRepoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(GetSampleTransactions());

            // Act
            var result = await _service.GetAllTransactionsAsync();

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ShouldReturnCorrectTransaction()
        {
            // Arrange
            var sample = GetSampleTransactions()[0];
            _transactionRepoMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(sample);

            // Act
            var result = await _service.GetTransactionByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("deposit", result.Type);
            Assert.Equal(100, result.Amount);
        }

        [Fact]
        public async Task GetTransactionsByTypeAsync_ShouldReturnFilteredTransactions()
        {
            // Arrange
            _transactionRepoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(GetSampleTransactions());

            // Act
            var result = await _service.GetTransactionsByTypeAsync("withdraw");

            // Assert
            Assert.Single(result);
            Assert.All(result, t => Assert.Equal("withdraw", t.Type));
        }

        [Fact]
        public async Task GetTransactionsByDateRangeAsync_ShouldReturnCorrectTransactions()
        {
            // Arrange
            _transactionRepoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(GetSampleTransactions());

            var from = new DateTime(2025, 8, 16);
            var to = new DateTime(2025, 8, 17);

            // Act
            var result = await _service.GetTransactionsByDateRangeAsync(from, to);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, t => Assert.InRange(t.Date, from, to));
        }
    }
}
