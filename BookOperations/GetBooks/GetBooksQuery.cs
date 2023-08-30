using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly BookstoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBooksQuery(BookstoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<BookViewModel> Handle()
    {
        var bookList = _dbContext.Books.OrderBy(bookList => bookList.Id).ToList<Book>();
        List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);
        /*new List<BookViewModel>();
        foreach (var book in bookList)
        {
            vm.Add(new BookViewModel()
            {
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PageCount = book.PageCount,
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
            });
        }*/

        return vm;
    }
    
    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}