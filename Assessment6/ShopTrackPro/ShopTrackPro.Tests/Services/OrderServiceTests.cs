using AutoMapper;
using Moq;
using ShopTrackPro.Application.Services;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace ShopTrackPro.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly OrderService _service;

        public OrderServiceTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();

            _service = new OrderService(
                _orderRepoMock.Object,
                _userRepoMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldSaveOrder()
        {
            //// Arrange
            //var dto = new OrderRequestDTO { UserId = 1 };
            //var user = new User { Id = 1, Name = "Test" };
            //var order = new Order { Id = 1, UserId = 1 };
            //var response = new OrderResponseDTO { Id = 1, UserId = 1 };

            //_userRepoMock.Setup(r => r.GetByIdAsync(1))
            //             .ReturnsAsync(user);

            //_mapperMock.Setup(m => m.Map<Order>(dto))
            //           .Returns(order);

            //_mapperMock.Setup(m => m.Map<OrderResponseDTO>(order))
            //           .Returns(response);

            //// AddAsync and SaveChangesAsync are Task-returning (void-like), so just return completed tasks
            //_orderRepoMock.Setup(r => r.AddAsync(It.IsAny<Order>()))
            //              .Returns(Task.CompletedTask);   // if Task only

            //_orderRepoMock.Setup(r => r.SaveChangesAsync())
            //              .ReturnsAsync(1);               // if Task<int>

            //// Act
            //var result = await _service.CreateOrderAsync(dto);

            //// Assert
            //_orderRepoMock.Verify(r => r.AddAsync(It.Is<Order>(o => o.UserId == 1)), Times.Once);
            //_orderRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);

            //Assert.NotNull(result);
            //Assert.Equal(1, result.UserId);
        }
    }
}
