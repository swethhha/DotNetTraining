using AutoMapper;
using Moq;
using ShopTrackPro.Application.Services;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Exceptions;
using ShopTrackPro.Core.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace ShopTrackPro.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ProductService(_productRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldThrow_WhenNameMissing()
        {
            var dto = new ProductRequestDTO { Price = 10 };
            await Assert.ThrowsAsync<ValidationException>(() => _service.CreateProductAsync(dto));
        }

        [Fact]
        public async Task CreateProductAsync_ShouldThrow_WhenPriceNegative()
        {
            var dto = new ProductRequestDTO { Name = "Test", Price = -1 };
            await Assert.ThrowsAsync<ValidationException>(() => _service.CreateProductAsync(dto));
        }

        [Fact]
        public async Task CreateProductAsync_ShouldSaveProduct()
        {
            var dto = new ProductRequestDTO { Name = "Book", Price = 20 };
            var entity = new Product { Id = 1, Name = "Book", Price = 20 };
            var response = new ProductResponseDTO { Id = 1, Name = "Book", Price = 20 };

            _mapperMock.Setup(m => m.Map<Product>(dto)).Returns(entity);
            _mapperMock.Setup(m => m.Map<ProductResponseDTO>(entity)).Returns(response);

            var result = await _service.CreateProductAsync(dto);

            _productRepoMock.Verify(r => r.AddAsync(entity), Times.Once);
            _productRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            Assert.Equal("Book", result.Name);
        }
    }
}
