using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorsById;

public class GetAuthorsByIdQuery
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;
    public int Id { get; set; }

    public GetAuthorsByIdQuery(BookstoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GetAuthorsByIdViewModel Handle()
    {
        var author = _context.Authors.Where(author=>author.Id==Id).SingleOrDefault();
        if (author is null)
        {
            throw new InvalidOperationException("The author does not exists.");
        }

        GetAuthorsByIdViewModel vm = _mapper.Map<GetAuthorsByIdViewModel>(author);
        return vm;
    }
    
}

public class GetAuthorsByIdViewModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
}