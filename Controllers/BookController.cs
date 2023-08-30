using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController:ControllerBase
{
    private readonly BookstoreDbContext _context;

    public BookController(BookstoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context);
        var result = query.Handle();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book = _context.Books.Where(book=>book.Id==id).SingleOrDefault();
        return book;
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        var book = _context.Books.SingleOrDefault(book => book.Title == newBook.Title);
        if (book is not null)
        {
            return BadRequest();
        }
        _context.Books.Add(newBook);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = _context.Books.SingleOrDefault(book => book.Id == id);
        if (book is null)
        {
            return BadRequest();
        }

        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

        _context.SaveChanges();
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Books.SingleOrDefault(book => book.Id == id);
        if (book is null)
        {
            return BadRequest();
        }

        _context.Books.Remove(book);
        _context.SaveChanges();
        return Ok();
    }
}