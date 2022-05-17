#pragma warning disable CS8618
namespace LibraryManager.Domain;

public record Author : BaseEntity
{ 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<Book> Books { get; set; } = new();
}