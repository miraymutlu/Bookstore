using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook;

public class DeleteBookCommand
{
    private readonly BookstoreDbContext _dbContext;
    public int BookId { get; set; }

    public DeleteBookCommand(BookstoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(book => book.Id == BookId);
        if (book is null)
        {
            throw new InvalidOperationException("The book does not exists.");
        }

        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }
}