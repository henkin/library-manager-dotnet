#pragma warning disable CS8618
namespace LibraryManager.Domain;

public class Author : BaseEntity
{ 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public virtual ICollection<Book> Books { get; set; }  
}