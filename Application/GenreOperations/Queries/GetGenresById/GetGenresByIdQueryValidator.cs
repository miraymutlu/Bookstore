using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenresById;

public class GetGenresByIdQueryValidator : AbstractValidator<GetGenresByIdQuery>
{
    public GetGenresByIdQueryValidator()
    {
        RuleFor(query => query.GenreId).GreaterThan(0);
    }
}