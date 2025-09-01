using Moq;
using ShopTrackPro.Application.Services;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Exceptions;
using ShopTrackPro.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShopTrackPro.Tests.Services
{
    public class OrderItemServiceTests
    {
        private readonly Mock<IOrderItemRepository> _orderItemRepoMock;
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly OrderItemService _service;

        public OrderItemServiceTests()
        {
            _orderItemRepoMock = new Mock<IOrderItemRepository>();
            _orderRepoMock = new Mock<IOrderRepository>();
            _productRepoMock = new Mock<IProductRepository>();
            _service = new OrderItemService(_orderItemRepoMock.Object, _orderRepoMock.Object, _productRepoMock.Object);
        }

        [Fact]
        public async Task AddOrderItemAsync_ShouldThrow_WhenQuantityInvalid()
        {
            var dto = new OrderItemRequestDTO { ProductId = 1, Quantity = 0 };
            await Assert.ThrowsAsync<ValidationException>(() => _service.AddOrderItemAsync(1, dto));
        }

        [Fact]
        public async Task AddOrderItemAsync_ShouldThrow_WhenOrderNotFound()
        {
            var dto = new OrderItemRequestDTO { ProductId = 1, Quantity = 1 };
            _orderRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Order)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _service.AddOrderItemAsync(1, dto));
        }

        [Fact]
        public async Task AddOrderItemAsync_ShouldAddNewItem()
        {
            var dto = new OrderItemRequestDTO { ProductId = 2, Quantity = 1 };
            var order = new Order { Id = 1 };
            var product = new Product { Id = 2, Name = "Book", Price = 10 };

            _orderRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(order);
            _productRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(product);
            _orderItemRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<OrderItem>());

            var result = await _service.AddOrderItemAsync(1, dto);

            _orderItemRepoMock.Verify(r => r.AddAsync(It.IsAny<OrderItem>()), Times.Once);
            _orderItemRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            Assert.Equal("Book", result.ProductName);
        }

        [Fact]
        public async Task GetOrderItemsByOrderIdAsync_ShouldReturnItems()
        {
            var order = new Order { Id = 1 };
            var items = new List<OrderItem>
            {
                new OrderItem { Id = 1, OrderId = 1, ProductId = 2, Quantity = 2, Product = new Product { Id = 2, Name = "Book", Price = 10 } }
            };

            _orderRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(order);
            _orderItemRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

            var result = await _service.GetOrderItemsByOrderIdAsync(1);

            Assert.Single(result);
            Assert.Equal("Book", result.First().ProductName);
        }

        [Fact]
        public async Task DeleteOrderItemAsync_ShouldThrow_WhenNotFound()
        {
            _orderItemRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((OrderItem)null);
            await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteOrderItemAsync(1));
        }
    }
}
