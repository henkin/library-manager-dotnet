using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace LibraryManager.WebApplication.Tests;

public class AuthorTests
{
    [Fact]
    public async Task Create_Unique_Returns200AndHeaderValue()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                // ... Configure test services
            });

        var client = application.CreateClient();
        
        
        // Act
        var response = await client.GetAsync("/authors");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        
        Assert.Equal("text/html; charset=utf-8", 
            response.Content.Headers.ContentType.ToString());
    }
}