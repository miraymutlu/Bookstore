using AutoMapper;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenresById;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;
using WebApi.Entities;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookCommand.CreateBookModel, Book>();
        CreateMap<Book, GetByIdQuery.BookByIdViewModel>().ForMember(dest => dest.Genre,
            opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        CreateMap<Book, GetBooksQuery.BookViewModel>().ForMember(dest => dest.Genre,
            opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        CreateMap<Genre, GetGenresQuery.GenresViewModel>();
        CreateMap<Genre, GetGenresByIdQuery.GenresByIdViewModel>();
    }
}