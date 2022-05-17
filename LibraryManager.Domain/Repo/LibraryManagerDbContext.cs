using LibraryManager.Domain;
using LibraryManager.Domain.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class LibraryManagerDbContext : DbContext//, IRepositoryProvider 
{  
    public DbSet<Book>? Books { get; set; }
    public DbSet<Author>? Authors { get; set; }
    public DbSet<Publisher>? Publishers { get; set; }

    private static string DbPath => Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
        "blogging.db"
        );

    public LibraryManagerDbContext() : this(new DbContextOptions<LibraryManagerDbContext>()) { }

    public LibraryManagerDbContext(DbContextOptions<LibraryManagerDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     new AuthorMap(modelBuilder.Entity<Author>());
    //     new BookMap(modelBuilder.Entity<Book>());
    // }

    // public IRepository<T> GetRepository<T>() where T : BaseEntity
    // {
    //     return new Repository<T>(this);
    // }
}

// public class BookMap
// {
//     public BookMap(EntityTypeBuilder<Book> entity)
//     {
//         throw new NotImplementedException();
//     }
// }
//
// public class AuthorMap
// {
//     public AuthorMap(EntityTypeBuilder<Author> entity)
//     {
//         
//     }
// }