
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;
namespace ToolTrack.Tests;

public class ToolApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ToolApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllTools_Returns200OK()
    {
        // Act
        var response = await _client.GetAsync("/api/tools");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    
[Fact]
public async Task GetToolById_WhenToolDoesNotExist_Returns404()
{
    // Act
    var response = await _client.GetAsync("/api/tools/999999");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}



[Fact]
public async Task GetToolById_WhenToolExists_Returns200()
{
    // Arrange: crear una herramienta
    var newTool = new
    {
        name = "Taladro de prueba",
        price = 150m,
        quantity = 5
    };

    var postResponse = await _client.PostAsJsonAsync(
        "/api/tools", newTool);

    postResponse.EnsureSuccessStatusCode();

    var json = await postResponse.Content.ReadAsStringAsync();
    using var document = JsonDocument.Parse(json);
    var id = document.RootElement.GetProperty("id").GetInt32();

    // Act
    var response = await _client.GetAsync($"/api/tools/{id}");

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
}

[Fact]
public async Task CreateTool_WithValidData_Returns201()
{
    // Arrange
    var newTool = new
    {
        name = "Llave de prueba",
        price = 80m,
        quantity = 3
    };

    // Act
    var response = await _client.PostAsJsonAsync(
        "/api/tools", newTool);

    // Assert
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
}

[Fact]
public async Task CreateTool_WithNameTooShort_Returns400()
{
    // Arrange
    var newTool = new
    {
        name = "AB",
        price = 100m,
        quantity = 2
    };

    // Act
    var response = await _client.PostAsJsonAsync(
        "/api/tools", newTool);

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
}

[Fact]
public async Task CreateTool_WithNegativePrice_Returns400()
{
    // Arrange
    var newTool = new
    {
        name = "Taladro inválido",
        price = -10m,
        quantity = 2
    };

    // Act
    var response = await _client.PostAsJsonAsync(
        "/api/tools", newTool);

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
}



[Fact]
public async Task UpdateTool_WhenToolExists_Returns204()
{
    // Arrange: crear herramienta
    var tool = new
    {
        name = "Taladro para actualizar",
        price = 150m,
        quantity = 5
    };

    var created = await _client.PostAsJsonAsync("/api/tools", tool);
    created.EnsureSuccessStatusCode();

    var json = await created.Content.ReadAsStringAsync();
    using var doc = JsonDocument.Parse(json);
    var id = doc.RootElement.GetProperty("id").GetInt32();

    var updatedTool = new
    {
        name = "Taladro actualizado",
        price = 200m,
        quantity = 8
    };

    // Act
    var response = await _client.PutAsJsonAsync(
        $"/api/tools/{id}", updatedTool);

    // Assert
    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
}

[Fact]
public async Task UpdateTool_WhenToolDoesNotExist_Returns404()
{
    var updatedTool = new
    {
        name = "Taladro actualizado",
        price = 200m,
        quantity = 8
    };

    var response = await _client.PutAsJsonAsync(
        "/api/tools/999999", updatedTool);

    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}

[Fact]
public async Task DeleteTool_WhenToolExists_Returns204()
{
    // Arrange
    var tool = new
    {
        name = "Taladro para eliminar",
        price = 150m,
        quantity = 5
    };

    var created = await _client.PostAsJsonAsync("/api/tools", tool);
    created.EnsureSuccessStatusCode();

    var json = await created.Content.ReadAsStringAsync();
    using var doc = JsonDocument.Parse(json);
    var id = doc.RootElement.GetProperty("id").GetInt32();

    // Act
    var response = await _client.DeleteAsync($"/api/tools/{id}");

    // Assert
    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
}

[Fact]
public async Task DeleteTool_WhenToolDoesNotExist_Returns404()
{
    var response = await _client.DeleteAsync("/api/tools/999999");

    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}


[Fact]
public async Task GetAllTools_ReturnsList()
{
    // Act
    var response = await _client.GetAsync("/api/tools");
    var content = await response.Content.ReadAsStringAsync();

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.False(string.IsNullOrWhiteSpace(content));
}

[Fact]
public async Task CreateTool_WithNegativeQuantity_Returns400()
{
    // Arrange
    var newTool = new
    {
        name = "Taladro inválido",
        price = 100m,
        quantity = -1
    };

    // Act
    var response = await _client.PostAsJsonAsync(
        "/api/tools", newTool);

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
}

[Fact]
public async Task DeleteTool_AfterDeletion_GetByIdReturns404()
{
    // Arrange: crear una herramienta
    var newTool = new
    {
        name = "Herramienta temporal",
        price = 100m,
        quantity = 2
    };

    var created = await _client.PostAsJsonAsync("/api/tools", newTool);
    created.EnsureSuccessStatusCode();

    var json = await created.Content.ReadAsStringAsync();
    using var doc = JsonDocument.Parse(json);
    var id = doc.RootElement.GetProperty("id").GetInt32();

    // Act: eliminar y consultar nuevamente
    var deleteResponse = await _client.DeleteAsync($"/api/tools/{id}");
    var getResponse = await _client.GetAsync($"/api/tools/{id}");

    // Assert
    Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
}


}
