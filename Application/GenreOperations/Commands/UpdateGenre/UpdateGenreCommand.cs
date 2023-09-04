using System.Globalization;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }
    
    private readonly BookstoreDbContext _context;

    public UpdateGenreCommand(BookstoreDbContext context)
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

        if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id == GenreId))
        {
            throw new InvalidOperationException("A book genre with the same name already exists");
        }

        genre.Name = Model.Name.Trim() == default ? Model.Name : genre.Name;
        genre.IsActive = Model.IsActive;
        _context.SaveChanges();
    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}