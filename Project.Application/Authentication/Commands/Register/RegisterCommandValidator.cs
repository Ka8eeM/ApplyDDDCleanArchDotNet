using FluentValidation;

namespace Project.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.firstName)
            .NotEmpty();
        RuleFor(x => x.lastName).NotEmpty();
        RuleFor(x => x.email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.password).NotEmpty();
    }
}