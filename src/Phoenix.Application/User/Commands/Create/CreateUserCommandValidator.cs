using FluentValidation;
using Phoenix.Domain;
using Utilities.Resources;

namespace Phoenix.Application.User.Commands.Create;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(f => f.Username)
            .NotNull()
            .WithMessage(nameof(CreateUserCommand.Username).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(CreateUserCommand.Username).IsRequiredMessage())
            .MaximumLength(Consts.USER_USERNAME.MaxLength)
            .WithMessage(nameof(CreateUserCommand.Username).StringLengthBetweenMessage(Consts.USER_USERNAME.MinLength, Consts.USER_USERNAME.MaxLength))
            .MinimumLength(Consts.USER_USERNAME.MinLength)
            .WithMessage(nameof(CreateUserCommand.Username).StringLengthBetweenMessage(Consts.USER_USERNAME.MinLength, Consts.USER_USERNAME.MaxLength));

        RuleFor(f => f.Email)
            .NotNull()
            .WithMessage(nameof(CreateUserCommand.Email).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(CreateUserCommand.Email).IsRequiredMessage())
            .EmailAddress()
            .WithMessage(nameof(CreateUserCommand.Email).FormatIncorrectMessage())
            .MaximumLength(Consts.EMAIL.MaxLength)
            .WithMessage(nameof(CreateUserCommand.Email).StringLengthBetweenMessage(Consts.EMAIL.MinLength, Consts.EMAIL.MaxLength))
            .MinimumLength(Consts.EMAIL.MinLength)
            .WithMessage(nameof(CreateUserCommand.Email).StringLengthBetweenMessage(Consts.EMAIL.MinLength, Consts.EMAIL.MaxLength));

    }
}