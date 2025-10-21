using FluentValidation;
using Phoenix.Domain;
using Utilities.Resources;

namespace Phoenix.Application.User.Commands.Update;

public class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage(nameof(UpdateUserCommand.UserId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(UpdateUserCommand.UserId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(UpdateUserCommand.UserId).FormatIncorrectMessage());
        
        RuleFor(f => f.Username)
            .NotNull()
            .WithMessage(nameof(UpdateUserCommand.Username).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(UpdateUserCommand.Username).IsRequiredMessage())
            .MaximumLength(Consts.USER_USERNAME.MaxLength)
            .WithMessage(nameof(UpdateUserCommand.Username).StringLengthBetweenMessage(Consts.USER_USERNAME.MinLength, Consts.USER_USERNAME.MaxLength))
            .MinimumLength(Consts.USER_USERNAME.MinLength)
            .WithMessage(nameof(UpdateUserCommand.Username).StringLengthBetweenMessage(Consts.USER_USERNAME.MinLength, Consts.USER_USERNAME.MaxLength));

        RuleFor(f => f.Email)
            .NotNull()
            .WithMessage(nameof(UpdateUserCommand.Email).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(UpdateUserCommand.Email).IsRequiredMessage())
            .EmailAddress()
            .WithMessage(nameof(UpdateUserCommand.Email).FormatIncorrectMessage())
            .MaximumLength(Consts.EMAIL.MaxLength)
            .WithMessage(nameof(UpdateUserCommand.Email).StringLengthBetweenMessage(Consts.EMAIL.MinLength, Consts.EMAIL.MaxLength))
            .MinimumLength(Consts.EMAIL.MinLength)
            .WithMessage(nameof(UpdateUserCommand.Email).StringLengthBetweenMessage(Consts.EMAIL.MinLength, Consts.EMAIL.MaxLength));

    }
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
    
}