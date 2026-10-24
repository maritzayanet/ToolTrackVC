using Moq;
using ToolTrack.Exceptions;
using ToolTrack.Model;
using ToolTrack.Repositories;
using ToolTrack.Services;
using Xunit;
using Microsoft.Extensions.Logging;

namespace ToolTrack.Tests;

public class ToolServiceTests
{
    [Fact]
    public async Task GetByIdAsync_WhenToolDoesNotExist_ThrowsException()
    {
        // Arrange: preparamos el repositorio simulado
        var mockRepository = new Mock<IToolRepository>();

        mockRepository
            .Setup(repository => repository.GetByIdAsync(999))
            .ReturnsAsync((Tool?)null);

        var mockLogger = Mock.Of<ILogger<ToolService>>();

        var service = new ToolService(
            mockRepository.Object,
            mockLogger
        );

        // Act + Assert: verificamos que lance la excepción
        await Assert.ThrowsAsync<ToolNotFoundException>(
            () => service.GetByIdAsync(999)
        );
    }



[Fact]
public async Task CreateAsync_WhenToolIsValid_ReturnsCreatedTool()
{
    // Arrange
    var mockRepository = new Mock<IToolRepository>();

    mockRepository
        .Setup(r => r.AddAsync(It.IsAny<Tool>()))
        .Callback<Tool>(tool => tool.Id = 1)
        .Returns(Task.CompletedTask);

    mockRepository
        .Setup(r => r.SaveChangesAsync())
        .Returns(Task.CompletedTask);

    var mockLogger = Mock.Of<ILogger<ToolService>>();

    var service = new ToolService(
        mockRepository.Object,
        mockLogger
    );
}

[Fact]
public async Task GetByIdAsync_WhenToolExists_ReturnsTool()
{
    // Arrange
    var tool = new Tool
    {
        Id = 1,
        Name = "Taladro DeWalt",
        Price = 150,
        Quantity = 5
    };

    var mockRepository = new Mock<IToolRepository>();

    mockRepository
        .Setup(r => r.GetByIdAsync(1))
        .ReturnsAsync(tool);

    var mockLogger = Mock.Of<ILogger<ToolService>>();

    var service = new ToolService(
        mockRepository.Object,
        mockLogger
    );

    // Act
    var result = await service.GetByIdAsync(1);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Taladro DeWalt", result.Name);
    Assert.Equal(150, result.Price);
}
[Fact]
public async Task CreateAsync_CallsRepositoryToAddTool()
{
    // Arrange
    var mockRepository = new Mock<IToolRepository>();

    mockRepository
        .Setup(r => r.AddAsync(It.IsAny<Tool>()))
        .Returns(Task.CompletedTask);

    mockRepository
        .Setup(r => r.SaveChangesAsync())
        .Returns(Task.CompletedTask);

    var mockLogger = Mock.Of<ILogger<ToolService>>();

    var service = new ToolService(
        mockRepository.Object,
        mockLogger
    );

    var tool = new Tool
    {
        Name = "Taladro Milwaukee",
        Price = 200,
        Quantity = 3
    };

    // Act
    await service.CreateAsync(tool);

    // Assert
    mockRepository.Verify(
        r => r.AddAsync(tool),
        Times.Once
    );
}

[Fact]
public async Task CreateAsync_SavesChangesOnce()
{
    // Arrange
    var mockRepository = new Mock<IToolRepository>();

    mockRepository
        .Setup(r => r.AddAsync(It.IsAny<Tool>()))
        .Returns(Task.CompletedTask);

    mockRepository
        .Setup(r => r.SaveChangesAsync())
        .Returns(Task.CompletedTask);

    var mockLogger = Mock.Of<ILogger<ToolService>>();

    var service = new ToolService(
        mockRepository.Object,
        mockLogger
    );

    var tool = new Tool
    {
        Name = "Llave DeWalt",
        Price = 50,
        Quantity = 2
    };

    // Act
    await service.CreateAsync(tool);

    // Assert
    mockRepository.Verify(
        r => r.SaveChangesAsync(),
        Times.Once
    );
}

}