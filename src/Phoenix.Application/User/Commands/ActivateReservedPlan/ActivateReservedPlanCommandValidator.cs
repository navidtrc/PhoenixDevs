using FluentValidation;
using Utilities.Resources;

namespace Phoenix.Application.User.Commands.ActivateReservedPlan;

public class ActivateReservedPlanCommandValidator: AbstractValidator<ActivateReservedPlanCommand>
{
    public ActivateReservedPlanCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage(nameof(ActivateReservedPlanCommand.UserId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(ActivateReservedPlanCommand.UserId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(ActivateReservedPlanCommand.UserId).FormatIncorrectMessage());
    }
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
    
}