using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Domain;

[Index(nameof(Email), IsUnique = true)]
public record Author : BaseEntity
{ 
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; set; }
    public List<Book> Books { get; } = new();
}