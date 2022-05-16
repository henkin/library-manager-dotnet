using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Domain;

public class BaseEntity 
{
    [Key]
    public Guid Id { get; set; }
}