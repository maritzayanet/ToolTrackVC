
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;
using ToolTrack.Exceptions;
using Xunit;

namespace ToolTrack.Tests;

public class GlobalExceptionHandlerTests
{
    [Fact]
    public async Task TryHandleAsync_ToolNotFound_Returns404()
    {
        // Arrange
        var logger = Mock.Of<ILogger<GlobalExceptionHandler>>();
        var handler = new GlobalExceptionHandler(logger);

        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.Request.Path = "/api/tools/999";

        var exception = new ToolNotFoundException("Herramienta no encontrada");

        // Act
        var handled = await handler.TryHandleAsync(
            context, exception, CancellationToken.None);

        // Assert
        Assert.True(handled);
        Assert.Equal(404, context.Response.StatusCode);

        context.Response.Body.Position = 0;
        var result = await JsonSerializer.DeserializeAsync<JsonElement>(
            context.Response.Body);

        Assert.Equal(404, result.GetProperty("status").GetInt32());
        Assert.Equal("Tool not found", result.GetProperty("title").GetString());
        Assert.Equal("/api/tools/999", result.GetProperty("instance").GetString());
    }

    [Fact]
    public async Task TryHandleAsync_UnexpectedException_Returns500()
    {
        // Arrange
        var logger = Mock.Of<ILogger<GlobalExceptionHandler>>();
        var handler = new GlobalExceptionHandler(logger);

        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.Request.Path = "/api/tools";

        var exception = new InvalidOperationException("Error interno de prueba");

        // Act
        var handled = await handler.TryHandleAsync(
            context, exception, CancellationToken.None);

        // Assert
        Assert.True(handled);
        Assert.Equal(500, context.Response.StatusCode);

        context.Response.Body.Position = 0;
        var result = await JsonSerializer.DeserializeAsync<JsonElement>(
            context.Response.Body);

        Assert.Equal(500, result.GetProperty("status").GetInt32());
        Assert.Equal("Internal Server Error", result.GetProperty("title").GetString());
        Assert.Equal(
            "Ocurrió un error inesperado.",
            result.GetProperty("detail").GetString());
    }
}
