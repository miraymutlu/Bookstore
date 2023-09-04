using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initalize(IServiceProvider serviceProvider)
    {
        using (var context =
               new BookstoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookstoreDbContext>>()))
        {
            //Look for any book.
            if (context.Books.Any())
            {
                return; //Data was already seeded.
            }
            context.Genres.AddRange(
                new Genre
                {
                    Name = "Personal Growth"
                },
                new Genre
                {
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Name = "Fantasy"
                    
            });
            
            context.Books.AddRange(
                new Book
                {
                    Title = "Lean Startup",
                    GenreId = 1, 
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)
                },
                new Book
                {
                    Title = "Herland",
                    GenreId = 2, 
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book
                {
                    Title = "Dune",
                    GenreId = 2,
                    PageCount = 540,
                    PublishDate = new DateTime(2001, 12, 21)
                });
            context.SaveChanges();
        }
    }
}