using EventEase.Application.Services;
using EventEase.Core.DTOs;
using EventEase.Core.Entities;
using EventEase.Core.Exceptions;
using EventEase.Core.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EventEase.Tests
{
    public class RegistrationServiceTests
    {
        private readonly Mock<IRegistrationRepository> _mockRepo;
        private readonly RegistrationService _service;

        public RegistrationServiceTests()
        {
            _mockRepo = new Mock<IRegistrationRepository>();
            _service = new RegistrationService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddRegistrationAsync_ShouldAddRegistration()
        {
            // Arrange
            var request = new RegistrationRequestDTO { UserId = 1, EventId = 2 };
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Registration>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddRegistrationAsync(request);

            // Assert
            Assert.True(result >= 0);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Registration>()), Times.Once);
        }

        [Fact]
        public async Task GetRegistrationByIdAsync_ShouldReturnRegistration()
        {
            // Arrange
            var reg = new Registration { Id = 1, UserId = 1, EventId = 2 };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(reg);

            // Act
            var result = await _service.GetRegistrationByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal(1, result.UserId);
            Assert.Equal(2, result.EventId);
        }

        [Fact]
        public async Task GetRegistrationByIdAsync_WhenNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Registration?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetRegistrationByIdAsync(1));
        }

        [Fact]
        public async Task GetAllRegistrationsAsync_ShouldReturnAllRegistrations()
        {
            // Arrange
            var regs = new List<Registration>
            {
                new Registration { Id = 1, UserId = 1, EventId = 2 },
                new Registration { Id = 2, UserId = 2, EventId = 2 }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(regs);

            // Act
            var result = await _service.GetAllRegistrationsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteRegistrationAsync_ShouldDeleteRegistration()
        {
            // Arrange
            var reg = new Registration { Id = 1, UserId = 1, EventId = 2 };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(reg);
            _mockRepo.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteRegistrationAsync(1);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteRegistrationAsync_WhenNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Registration?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteRegistrationAsync(1));
        }
    }
}
