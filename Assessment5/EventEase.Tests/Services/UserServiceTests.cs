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
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _service = new UserService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddUserAsync_ShouldAddUser()
        {
            // Arrange
            var request = new UserRequestDTO { Name = "John Doe", Email = "john@example.com" };
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddUserAsync(request);

            // Assert
            Assert.True(result >= 0);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _service.GetUserByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public async Task GetUserByIdAsync_WhenNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((User?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetUserByIdAsync(1));
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _service.GetAllUsersAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldDeleteUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
            _mockRepo.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteUserAsync(1);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((User?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteUserAsync(1));
        }
    }
}
