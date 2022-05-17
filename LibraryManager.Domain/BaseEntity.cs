using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Domain;

public abstract record BaseEntity 
{
    [Key]
    public Guid Id { get; set; }
}