using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    public readonly BookstoreDbContext _context;
    public readonly IMapper _mapper;
    
    public GetGenresQuery(BookstoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<GenresViewModel> Handle()
    {
        var genres = _context.Genres.Where(genres => genres.IsActive).OrderBy(genres => genres.Id);
        List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
        return returnObj;
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}