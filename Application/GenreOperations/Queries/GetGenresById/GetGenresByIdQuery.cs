using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenresById;

public class GetGenresByIdQuery
{
    public int GenreId { get; set; }
    public readonly BookstoreDbContext _context;
    public readonly IMapper _mapper;
    
    public GetGenresByIdQuery(BookstoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GenresByIdViewModel Handle()
    {
        var genre = _context.Genres.SingleOrDefault(genre => genre.IsActive && genre.Id == GenreId);
        if (genre is null)
        {
            throw new InvalidOperationException("The book genre not found.");
        }
        return _mapper.Map<GenresByIdViewModel>(genre);
    }

    public class GenresByIdViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}