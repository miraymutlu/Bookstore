using FluentValidation;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorsById;

public class GetAuthorsByIdQueryValidator : AbstractValidator<GetAuthorsByIdQuery>
{
    public GetAuthorsByIdQueryValidator()
    {
        RuleFor(query => query.Id).GreaterThan(0);
    }
}