using FluentValidation;
using Utilities.Resources;

namespace Phoenix.Application.SubscriptionPlan.Commands.Activate;

public class ActivateSubscriptionPlanCommandValidator: AbstractValidator<ActivateSubscriptionPlanCommand>
{
    public ActivateSubscriptionPlanCommandValidator()
    {
        RuleFor(x => x.PlanId)
            .NotNull()
            .WithMessage(nameof(ActivateSubscriptionPlanCommand.PlanId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(ActivateSubscriptionPlanCommand.PlanId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(ActivateSubscriptionPlanCommand.PlanId).FormatIncorrectMessage());
    }
    
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
}