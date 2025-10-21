using FluentValidation;
using Utilities.Resources;

namespace Phoenix.Application.SubscriptionPlan.Commands.MarkPending;

public class MarkPendingSubscriptionPlanCommandValidator: AbstractValidator<MarkPendingSubscriptionPlanCommand>
{
    public MarkPendingSubscriptionPlanCommandValidator()
    {
        RuleFor(x => x.PlanId)
            .NotNull()
            .WithMessage(nameof(MarkPendingSubscriptionPlanCommand.PlanId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(MarkPendingSubscriptionPlanCommand.PlanId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(MarkPendingSubscriptionPlanCommand.PlanId).FormatIncorrectMessage());
    }
    
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
}