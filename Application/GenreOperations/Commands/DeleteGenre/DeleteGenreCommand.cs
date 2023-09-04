using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommand
{
    public int GenreId { get; set; }

    private readonly BookstoreDbContext _context;

    public DeleteGenreCommand(BookstoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(genre => genre.Id == GenreId);
        if (genre is null)
        {
            throw new InvalidOperationException("The book genre not found.");
        }

        _context.Genres.Remove(genre);
        _context.SaveChanges();
    }
}