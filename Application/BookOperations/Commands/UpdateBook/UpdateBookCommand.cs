using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook;

public class UpdateBookCommand
{
    public UpdateBookModel Model { get; set; }
    public int BookId { get; set; }
    
    private readonly BookstoreDbContext _dbContext;

    public UpdateBookCommand(BookstoreDbContext dbContext)
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

        book.Title = Model.Title != default ? Model.Title : book.Title;
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
        book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;

        _dbContext.SaveChanges();
    }
    
    public class UpdateBookModel
    {
        public int GenreId { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}