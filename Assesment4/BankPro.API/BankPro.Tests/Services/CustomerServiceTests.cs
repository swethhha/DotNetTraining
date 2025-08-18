using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankPro.Application.Services;
using BankPro.Core.Entities;
using BankPro.Core.Interfaces;
using BankPro.Core.DTOs;
using AutoMapper;

namespace BankPro.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _mapperMock = new Mock<IMapper>();
            _customerService = new CustomerService(_customerRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldAddAndReturnCustomer()
        {
            var dto = new CustomerRequestDTO { Name = "John", Email = "john@test.com" };

            _customerRepoMock.Setup(r => r.AddAsync(It.IsAny<Customer>()))
                             .Returns(Task.CompletedTask);

            var result = await _customerService.AddCustomerAsync(dto);

            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Email, result.Email);
            _customerRepoMock.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer_WhenExists()
        {
            var customer = new Customer { Id = 1, Name = "Alice", Email = "alice@test.com" };
            _customerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(customer);

            var result = await _customerService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Alice", result.Name);
            Assert.Equal("alice@test.com", result.Email);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "A", Email = "a@test.com" }
            };

            _customerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);

            var result = await _customerService.GetAllAsync();

            Assert.Single(result);
            Assert.Equal("A", result.First().Name);
            Assert.Equal("a@test.com", result.First().Email);
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldCallRepository()
        {
            await _customerService.DeleteCustomerAsync(1);
            _customerRepoMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task UpdateCustomerAsync_ShouldUpdate_WhenExists()
        {
            var dto = new CustomerRequestDTO { Name = "Updated", Email = "updated@test.com" };
            var existing = new Customer { Id = 1, Name = "Old", Email = "old@test.com" };

            _customerRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

            await _customerService.UpdateCustomerAsync(1, dto);

            _mapperMock.Verify(m => m.Map(dto, existing), Times.Once);
            _customerRepoMock.Verify(r => r.UpdateAsync(existing), Times.Once);
        }
    }
}
