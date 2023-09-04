using FluentValidation;

namespace WebApi.BookOperations.GetById;

public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
        RuleFor(command => command.BookID).GreaterThan(0);
    }
}