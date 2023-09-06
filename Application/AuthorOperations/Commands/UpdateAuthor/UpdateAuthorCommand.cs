using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    public int Id { get; set; }
    public UpdateAuthorViewModel Model { get; set; }
    private readonly BookstoreDbContext _context;

    public UpdateAuthorCommand(BookstoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(author => author.Id == Id);
        if (author is null)
        {
            throw new InvalidOperationException("The author does not exists.");
        }

        author.Name = Model.Name != default ? Model.Name : author.Name;
        author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
        author.Birthday = Model.Birthday != default ? Model.Birthday : author.Birthday;
        
        _context.SaveChanges();
    }
}

public class UpdateAuthorViewModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
}