using WebApi.BookOperations.GetBooks;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetById;

public class GetByIdQuery
{
    private readonly BookstoreDbContext _dbContext;
    public int BookID { get; set; }

    public GetByIdQuery(BookstoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public BookByIdViewModel Handle()
    {
        var book = _dbContext.Books.Where(book=>book.Id==BookID).SingleOrDefault();
        if (book is null)
        {
            throw new InvalidOperationException("The book does not exists.");
        }
        BookByIdViewModel vm = new BookByIdViewModel();
        vm.Title = book.Title;
        vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
        vm.PageCount = book.PageCount;
        vm.Genre = ((GenreEnum)book.GenreId).ToString();
        return vm;
    } 
    public class BookByIdViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}