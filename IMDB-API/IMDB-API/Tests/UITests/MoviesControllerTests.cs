using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class MoviesControllerTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public MoviesControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/api/movies")]
    [InlineData("/api/movies/tt0816692")]
    [InlineData("/api/movies/batch?ids=tt0468569&ids=tt0816692")]
    [InlineData("/api/movies/tt0816692/related")]
    public async Task GetEndpoints_OnSuccess_ReturnCorrectContentType(
        string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
    }
}