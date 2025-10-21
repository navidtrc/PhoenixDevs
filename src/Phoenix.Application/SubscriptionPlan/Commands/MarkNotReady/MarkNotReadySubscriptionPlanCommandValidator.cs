using FluentValidation;
using Utilities.Resources;

namespace Phoenix.Application.SubscriptionPlan.Commands.MarkNotReady;

public class MarkNotReadySubscriptionPlanCommandValidator: AbstractValidator<MarkNotReadySubscriptionPlanCommand>
{
    public MarkNotReadySubscriptionPlanCommandValidator()
    {
        RuleFor(x => x.PlanId)
            .NotNull()
            .WithMessage(nameof(MarkNotReadySubscriptionPlanCommand.PlanId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(MarkNotReadySubscriptionPlanCommand.PlanId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(MarkNotReadySubscriptionPlanCommand.PlanId).FormatIncorrectMessage());
    }
    
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
}