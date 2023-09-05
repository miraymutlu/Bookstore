using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries;

public class GetAuthorsQuery
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(BookstoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public List<AuthorsViewModel> Handle()
    {
        var authorsList = _context.Authors.OrderBy(author => author.Id).ToList<Author>();
        List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorsList);
        return vm;
    }
}

public class AuthorsViewModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
}