using FluentValidation;
using Phoenix.Application.SubscriptionPlan.Commands.Activate;
using Utilities.Resources;

namespace Phoenix.Application.SubscriptionPlan.Commands.Deactivate;

public class DeactivateSubscriptionPlanCommandValidator: AbstractValidator<DeactivateSubscriptionPlanCommand>
{
    public DeactivateSubscriptionPlanCommandValidator()
    {
        RuleFor(x => x.PlanId)
            .NotNull()
            .WithMessage(nameof(DeactivateSubscriptionPlanCommand.PlanId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(DeactivateSubscriptionPlanCommand.PlanId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(DeactivateSubscriptionPlanCommand.PlanId).FormatIncorrectMessage());
    }
    
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
}