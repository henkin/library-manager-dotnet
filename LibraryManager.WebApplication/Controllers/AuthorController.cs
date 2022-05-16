using LibraryManager.Domain;
using LibraryManager.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.WebApplication.Controllers;

[Route("[controller]")]
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
    public async Task<Author> Get(Guid id)
    {
        var author = await _authorRepository.Get(id);
        return author;
    }

    // POST: /authors
    [HttpPost]
    public async Task<Guid> Post([FromForm] AuthorModel value)
    {
        var id = await _authorRepository.Insert(value);
        return id;
    }

    // PUT: /authors/5
    [HttpPut("{id}")]
    public async Task Put([FromRoute] Guid id, [FromForm] AuthorModel value)
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