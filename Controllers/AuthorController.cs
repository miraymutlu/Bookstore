using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsById;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;

    public AuthorController(BookstoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetAuthorsById(int id)
    {
        GetAuthorsByIdViewModel result;
        GetAuthorsByIdQuery query = new GetAuthorsByIdQuery(_context, _mapper);
        query.Id = id;
        GetAuthorsByIdQueryValidator validator = new GetAuthorsByIdQueryValidator();
        validator.ValidateAndThrow(query);
        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddAuthor([FromBody] CreateAuthorViewModel newBook)
    {
        CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
        command.Model = newBook;
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorViewModel updatedAuthor)
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        command.Id = id;
        command.Model = updatedAuthor;
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.Id = id;
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }
}