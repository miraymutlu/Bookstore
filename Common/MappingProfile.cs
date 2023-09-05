using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthorCommand;
using WebApi.Application.AuthorOperations.Queries;
using WebApi.Application.AuthorOperations.Queries.GetAuthorsById;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenresById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;
using WebApi.Entities;
using CreateBookCommand = WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookCommand.CreateBookModel, Book>();
        CreateMap<Book, GetByIdQuery.BookByIdViewModel>().ForMember(dest => dest.Genre,
            opt => opt.MapFrom(src => src.Genre.Name));
        CreateMap<Book, GetBooksQuery.BookViewModel>().ForMember(dest => dest.Genre,
            opt => opt.MapFrom(src => src.Genre.Name));
        CreateMap<Genre, GetGenresQuery.GenresViewModel>();
        CreateMap<Genre, GetGenresByIdQuery.GenresByIdViewModel>();
        CreateMap<CreateAuthorViewModel, Author>();
        CreateMap<Author, AuthorsViewModel>();
        CreateMap<Author, GetAuthorsByIdViewModel>();
    }
}