using LibraryManager.Domain;
using Microsoft.EntityFrameworkCore;


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
}
