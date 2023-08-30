using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;
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
    public IActionResult GetById(int id)
    {
        GetByIdQuery.BookByIdViewModel result;
        try
        {
            GetByIdQuery query = new GetByIdQuery(_context);
            query.BookID = id;
            result = query.Handle();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookCommand.CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(_context);
        try
        {
            command.Model = newBook;
            command.Handle();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        
        
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