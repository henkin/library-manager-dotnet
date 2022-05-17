using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Text.Json;
using LibraryManager.Domain;
using LibraryManager.WebApplication.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace LibraryManager.WebApplication.Tests;

/// <summary>
/// Using https://github.com/adrianiftode/FluentAssertions.Web 
/// </summary>
public class AuthorTests
{
    private static readonly string RequestUri = "/authors";
    private static readonly AuthorApiModel TestResource = new()
    {
        Email = "test@testing.io",
        FirstName = "First",
        LastName = "Last"
    };

    protected HttpClient AppClient { get; }
    
    public AuthorTests()
    {   
        // delete SQLite DB before each test
        var db = new LibraryManagerDbContext();
        db.Database.EnsureDeleted();
        
        // create integration test Server and its HttpClient
        AppClient = new WebApplicationFactory<Program>()
            .CreateClient();
    }


    [Fact]
    public async Task GetAll_Returns200_EmptyCollection()
    {
        // Act
        var response = await AppClient.GetAsync(RequestUri);

        // Assert
        response.Should().Be200Ok().And.BeAs(new List<Author>());
    }

    [Fact]
    public async Task Create_Duplicate_Returns400_Error()
    {
        // Arrange
        var postResponse = await PostAsync(TestResource, RequestUri);
        postResponse.Should().Be200Ok();
        
        // Act
        postResponse = await PostAsync(TestResource, RequestUri);
        
        // Assert
        postResponse.Should().Be400BadRequest().And.HaveErrorMessage("*Duplicate*");
    }
    
    [Fact]
    public async Task Create_Malformed_Returns400_Error()
    {
        // Arrange
        var malformedResource = new { badProperty = "don't like" };
        
        // Act
        var postResponse = await PostAsync(malformedResource, RequestUri);
        
        // Assert
        postResponse.Should().Be400BadRequest().And.HaveErrorMessage("*field is required*");
    }

    [Fact]
    public async Task Create_Unique_Returns200_Id()
    {
        // Arrange
        
        // Act
        var postResponse = await PostAsync(TestResource, RequestUri);
        postResponse.Should().Be200Ok();

        // Assert
        //var content = await getResponse.Content.ReadAsStringAsync();
        var getResponse = await AppClient.GetAsync(RequestUri);
        getResponse
            .Should()
            .Be200Ok().And
            .Satisfy<IList<Author>>(authors => authors
                .Should().ContainSingle().Which // https://fluentassertions.com/collections/
                .Should().BeEquivalentTo(TestResource, options => 
                    options
                        .Excluding(o => o.Id)
                        .Excluding(o => o.Books)
                    ) // ignore ID, as we didn't know it when posting
                // https://fluentassertions.com/objectgraphs/#selecting-members
            ); 
    }

    [Fact]
    public async Task Get_Existing_Returns200_Item()
    {        
        // Arrange
        var postResponse = await PostAsync(TestResource, RequestUri);
        var id = await DeserializeResponse<Guid>(postResponse);
        
        // Act
        var getResponse = await AppClient.GetAsync($"${RequestUri}/${id}");
        
        // Assert
        getResponse.Should().Be200Ok().And.BeAs(
          TestResource with { Id = id }
        );
    }

    [Fact]
    public async Task Get_Missing_Returns404_Error()
    {
    }

    [Fact]
    public async Task Update_Existing_Returns202_Item()
    {
    }
    
    [Fact]
    public async Task Update_Missing_Returns404_Item()
    {
    }
    
    [Fact]
    public async Task Update_Malformed_Returns400_Item()
    {
    }
    
    private async Task<T> DeserializeResponse<T>(HttpResponseMessage message)
    {
        var responseString = await message.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseString); 
    }
    
    private async Task<HttpResponseMessage> PostAsync(object data, string requestUri)
    {
        return await AppClient.PostAsync(requestUri, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));
    }
}