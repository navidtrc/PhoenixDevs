using FluentValidation;
using Utilities.Resources;

namespace Phoenix.Application.User.Commands.RemoveUserPlan;

public class RemoveUserPlanCommandValidator: AbstractValidator<RemoveUserPlanCommand>
{
    public RemoveUserPlanCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage(nameof(RemoveUserPlanCommand.UserId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(RemoveUserPlanCommand.UserId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(RemoveUserPlanCommand.UserId).FormatIncorrectMessage());
    }
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
    
}