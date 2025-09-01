using AutoMapper;
using Moq;
using ShopTrackPro.Application.Services;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Exceptions;
using ShopTrackPro.Core.Interfaces;
using System.Threading.Tasks;
using Xunit;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new UserService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsUser_WhenExists()
    {
        var user = new User { Id = 1, Name = "Alice" };
        var userResponse = new UserResponseDTO { Id = 1, Name = "Alice" };

        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
        _mockMapper.Setup(m => m.Map<UserResponseDTO>(user)).Returns(userResponse);

        var result = await _service.GetUserByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Alice", result!.Name);
    }

    [Fact]
    public async Task CreateUserAsync_CallsAddAndSave()
    {
        var userDto = new UserRequestDTO { Name = "Bob", Email = "bob@test.com", Password = "Secret123" };
        var user = new User { Id = 2, Name = "Bob", Email = "bob@test.com" };
        var userResponse = new UserResponseDTO { Id = 2, Name = "Bob" };

        _mockMapper.Setup(m => m.Map<User>(userDto)).Returns(user);
        _mockMapper.Setup(m => m.Map<UserResponseDTO>(user)).Returns(userResponse);

        var result = await _service.CreateUserAsync(userDto);

        _mockRepo.Verify(r => r.AddAsync(user), Times.Once);
        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        Assert.Equal("Bob", result!.Name);
    }

    [Fact]
    public async Task UpdateUserAsync_ThrowsNotFound_WhenNotFound()
    {
        var userDto = new UserRequestDTO { Name = "Charlie", Email = "charlie@test.com", Password = "Secret123" };
        _mockRepo.Setup(r => r.GetByIdAsync(3)).ReturnsAsync((User?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateUserAsync(3, userDto));
    }

    [Fact]
    public async Task DeleteUserAsync_ThrowsNotFound_WhenNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(10)).ReturnsAsync((User?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteUserAsync(10));
    }

    [Fact]
    public async Task DeleteUserAsync_ReturnsTrue_WhenDeleted()
    {
        var user = new User { Id = 5, Name = "David", Email = "david@test.com" };
        _mockRepo.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(user);

        var result = await _service.DeleteUserAsync(5);

        Assert.True(result);
        _mockRepo.Verify(r => r.Delete(user), Times.Once);
        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
