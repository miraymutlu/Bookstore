using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateBook;

public class CreateBookCommand
{
    public CreateAuthorViewModel Model { get; set; }
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateBookCommand(BookstoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(author => author.Name == Model.Name && author.Surname == Model.Surname);
        if (author is not null)
        {
            throw new InvalidOperationException("The author is already exists.");
        }

        author = _mapper.Map<Author>(Model);
        _context.Authors.Add(author);
        _context.SaveChanges();
    }
}

public class CreateAuthorViewModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
}