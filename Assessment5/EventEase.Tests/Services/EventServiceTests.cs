using EventEase.Application.Services;
using EventEase.Core.DTOs;
using EventEase.Core.Entities;
using EventEase.Core.Exceptions;
using EventEase.Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EventEase.Tests
{
    public class EventServiceTests
    {
        private readonly Mock<IEventRepository> _mockRepo;
        private readonly EventService _service;

        public EventServiceTests()
        {
            _mockRepo = new Mock<IEventRepository>();
            _service = new EventService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddEventAsync_ValidRequest_ReturnsEventId()
        {
            // Arrange
            var request = new EventRequestDTO
            {
                Title = "Test Event",
                Description = "Desc",
                Location = "Bangalore",
                Date = DateTime.Now
            };
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Event>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddEventAsync(request);

            // Assert
            Assert.True(result >= 0);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Event>()), Times.Once);
        }

        [Fact]
        public async Task GetEventByIdAsync_EventExists_ReturnsEventResponseDTO()
        {
            // Arrange
            var ev = new Event
            {
                Id = 1,
                Title = "Event1",
                Description = "Desc",
                Location = "Bangalore",
                Date = DateTime.Now
            };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(ev);

            // Act
            var result = await _service.GetEventByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ev.Title, result.Title);
        }

        [Fact]
        public async Task GetEventByIdAsync_EventDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Event?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetEventByIdAsync(1));
        }

        [Fact]
        public async Task GetAllEventsAsync_ReturnsListOfEvents()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event { Id=1, Title="E1", Description="D1", Location="B", Date=DateTime.Now },
                new Event { Id=2, Title="E2", Description="D2", Location="M", Date=DateTime.Now }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(events);

            // Act
            var result = await _service.GetAllEventsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateEventAsync_EventExists_UpdatesEvent()
        {
            // Arrange
            var ev = new Event
            {
                Id = 1,
                Title = "Old Title",
                Description = "Old Desc",
                Location = "Old Loc",
                Date = DateTime.Now
            };
            var request = new EventRequestDTO
            {
                Title = "New Title",
                Description = "New Desc",
                Location = "New Loc",
                Date = DateTime.Now.AddDays(1)
            };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(ev);
            _mockRepo.Setup(r => r.UpdateAsync(ev)).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateEventAsync(1, request);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(It.Is<Event>(e => e.Title == "New Title")), Times.Once);
        }

        [Fact]
        public async Task DeleteEventAsync_EventExists_DeletesEvent()
        {
            // Arrange
            var ev = new Event { Id = 1, Title = "E1", Description = "D", Location = "L", Date = DateTime.Now };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(ev);
            _mockRepo.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteEventAsync(1);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }
}
