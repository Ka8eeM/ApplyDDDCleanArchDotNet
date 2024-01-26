
using FluentValidation;

namespace Project.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotEmpty();

        RuleFor(u => u.FirstName)
            .NotEmpty()
            .NotNull();

        RuleFor(u => u.LastName)
            .NotEmpty()
            .NotNull();

    }
}