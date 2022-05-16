using LibraryManager.Domain;
using LibraryManager.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.WebApplication.Controllers;

[Route("authors")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IRepository<Author> _authorRepository;

    public AuthorController(IRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }
        
    [HttpGet]
    public async Task<List<Author>> Get()
    {
        var authors = await _authorRepository.GetAll().ToListAsync();
        return authors;
    }
        
    // GET: api/Author/5
    [HttpGet("{id}")]
    public async Task<Author> Get(Guid id)
    {
        var author = await _authorRepository.Get(id);
        return author;
    }

    // POST: api/Author
    [HttpPost]
    public async Task<Guid> Post([FromBody] AuthorModel value)
    {
        var id = await _authorRepository.Insert(value);
        return id;
    }

    // PUT: api/Author/5
    [HttpPut("{id}")]
    public async Task Put([FromRoute] Guid id, [FromBody] AuthorModel value)
    {
        var entity = await _authorRepository.Get(id);
        await _authorRepository.Update(entity);
    }

    // DELETE: api/Author/5
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _authorRepository.Delete(id);
    }
}