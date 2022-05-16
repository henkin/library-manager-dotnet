namespace LibraryManager.Domain;

public class Book : BaseEntity
{
    public string Name { get; set; }
    public string ISBN { get; set; }
    public virtual Publisher Publisher { get; set; }
    public virtual Author Author { get; set; }  
}