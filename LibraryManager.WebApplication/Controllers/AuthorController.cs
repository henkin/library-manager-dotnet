using LibraryManager.Domain;
using LibraryManager.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.WebApplication.Controllers;

[Route("/[controller]")]
[ApiController]
public class AuthorsController : Controller
{
    private readonly IRepository<Author> _authorRepository;

    public AuthorsController(IRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }
        
    [HttpGet]
    public async Task<List<Author>> Get()
    {
        var authors = await _authorRepository.GetAll().ToListAsync();
        return authors;
    }
        
    // GET: /authors
    [HttpGet("{id}")]
    public async Task<Author> Get(string id)
    {
        var author = await _authorRepository.Get(new Guid(id));
        return author;
    }
    
    // POST: /authors
    [HttpPost]
    public async Task<IActionResult> Post(AuthorApiModel model)
    {
        if (model.Id != Guid.Empty)
        {
            return Problem("Id must not be provided", statusCode: 400);
        }
        
        Guid id;
        try
        {
            id = await _authorRepository.Insert(model);
            return Ok(id);
        }
        catch (DbUpdateException e)
        {
            // it tried to update instead of inserting, email is being reused 
            //return Problem(statusCode: 400, title: "Email must be unique");
            return Problem("Duplicate email, it must be unique", statusCode: 400);
        }
    }
    
    // PUT: /authors/5
    [HttpPut("{id}")]
    public async Task Put([FromRoute] Guid id, [FromForm] AuthorApiModel value)
    {
        var entity = await _authorRepository.Get(id);
        await _authorRepository.Update(entity);
    }

    // DELETE: /authors/5
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _authorRepository.Delete(id);
    }
}