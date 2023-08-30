using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;
using WebApi.BookOperations.UpdateBook;
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
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookCommand.UpdateBookModel updatedBook)
    {
        try
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
            command.Handle();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            command.Handle();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }
}