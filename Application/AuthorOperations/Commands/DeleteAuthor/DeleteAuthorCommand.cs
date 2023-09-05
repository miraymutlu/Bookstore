using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    public int Id { get; set; }

    private readonly BookstoreDbContext _context;

    public DeleteAuthorCommand(BookstoreDbContext context)
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

        _context.Authors.Remove(author);
        _context.SaveChanges();
    }
}