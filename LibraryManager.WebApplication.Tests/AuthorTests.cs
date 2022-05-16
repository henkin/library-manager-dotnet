using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using System.Text.Json;
using LibraryManager.Domain;
using LibraryManager.WebApplication.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace LibraryManager.WebApplication.Tests;

public class AuthorTests
{
    public HttpClient AppClient { get; }
    
    public AuthorTests()
    {   
        AppClient = new WebApplicationFactory<Program>()
            .CreateClient();
    }


    [Fact]
    public async Task GetAll_Returns200_EmptyCollection()
    {
        // Act
        var response = await AppClient.GetAsync("/authors");

        // Assert
        response.Should().Be200Ok().And.BeAs(new List<Author>());
    }
    
    [Fact]
    public async Task Create_Unique_Returns200_WithCreatedId()
    {
        // Arrange
        var author = new AuthorModel()
        {
            Email = "test@testing.io",
            FirstName = "First",
            LastName = "Last"
        };

        // Act
        var postResponse = await AppClient.PostAsync("/authors", new StringContent(JsonSerializer.Serialize(author)));
        
        // Assert
        var getResponse = await AppClient.GetAsync("/authors");
        getResponse.Should().Be200Ok().And.BeAs(new [] { author }); // Status Code 200-299
   }
}