using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations;

public class BookstoreDbContext : DbContext
{
    public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options)
    {}
    public DbSet<Book> Books { get; set; }
    
}