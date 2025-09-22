using Xunit;
using Moq;
using FluentAssertions;
using YourProject.Domain.Entities;
using YourProject.Domain.Interfaces;
using YourProject.Application.Services;

namespace YourProject.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsUser_WhenExists()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(1))
                    .ReturnsAsync(new User { Id = 1, Name = "Divya" });

            var service = new UserService(mockRepo.Object);

            var result = await service.GetByIdAsync(1);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Divya");
        }
    }
}
