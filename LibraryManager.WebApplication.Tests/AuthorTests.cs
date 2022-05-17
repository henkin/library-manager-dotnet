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
    private AuthorModel _testResource;
    private readonly AuthorModel _authorModel;
    public HttpClient AppClient { get; }
    
    public AuthorTests()
    {   
        // delete SQLite DB before each test
        var db = new LibraryManagerDbContext();
        db.Database.EnsureDeleted();
        
        // create integration test Server and its HttpClient
        AppClient = new WebApplicationFactory<Program>()
            .CreateClient();
        _authorModel = _testResource = new AuthorModel()
        {
            Email = "test@testing.io",
            FirstName = "First",
            LastName = "Last"
        };
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
        throw new NotImplementedException();
    }
    
    [Fact]
    public async Task Create_Malformed_Returns400_Error()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task Create_Unique_Returns200_Id()
    {
        // Arrange
        var postResponse = await PostAsync(_testResource, RequestUri);
        postResponse.Should().Be200Ok();
        
        // Act
        var getResponse = await AppClient.GetAsync(RequestUri);

        // Assert
        //var content = await getResponse.Content.ReadAsStringAsync();
        getResponse
            .Should()
            .Be200Ok().And
            .Satisfy<IList<Author>>(authors => authors
                .Should().ContainSingle().Which // https://fluentassertions.com/collections/
                .Should().BeEquivalentTo(_testResource, options => 
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
        throw new NotImplementedException();
    }

    [Fact]
    public async Task Get_Missing_Returns404_Error()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task Update_Existing_Returns202_Item()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public async Task Update_Missing_Returns404_Item()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public async Task Update_Malformed_Returns400_Item()
    {
        throw new NotImplementedException();
    }
    
    private async Task<HttpResponseMessage> PostAsync(object data, string requestUri)
    {
        return await AppClient.PostAsync(requestUri, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));
    }
}