using FluentValidation;
using Phoenix.Domain;
using Utilities.Resources;

namespace Phoenix.Application.SubscriptionPlan.Commands.Update;

public class UpdateSubscriptionPlanCommandValidator: AbstractValidator<UpdateSubscriptionPlanCommand>
{
    public UpdateSubscriptionPlanCommandValidator()
    {
        RuleFor(x => x.PlanId)
            .NotNull()
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.PlanId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.PlanId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.PlanId).FormatIncorrectMessage());
        
        RuleFor(f => f.Title)
            .NotNull()
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Title).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Title).IsRequiredMessage())
            .MaximumLength(Consts.SUBSCRIPTION_PLAN_TITLE.MaxLength)
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Title).StringLengthBetweenMessage(Consts.SUBSCRIPTION_PLAN_TITLE.MinLength, Consts.SUBSCRIPTION_PLAN_TITLE.MaxLength))
            .MinimumLength(Consts.SUBSCRIPTION_PLAN_TITLE.MinLength)
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Title).StringLengthBetweenMessage(Consts.SUBSCRIPTION_PLAN_TITLE.MinLength, Consts.SUBSCRIPTION_PLAN_TITLE.MaxLength));

        RuleFor(f => f.Description)
            .NotNull()
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Description).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Description).IsRequiredMessage())
            .MaximumLength(Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MaxLength)
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Description).StringLengthBetweenMessage(Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MinLength, Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MaxLength))
            .MinimumLength(Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MinLength)
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Description).StringLengthBetweenMessage(Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MinLength, Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MaxLength));

        RuleFor(f => f.Price)
            .NotNull()
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Price).IsRequiredMessage())
            .GreaterThanOrEqualTo(Consts.SUBSCRIPTION_PLAN_PRICE.MinValue)
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Price).ValueBetweenMessage(Consts.SUBSCRIPTION_PLAN_PRICE.MinValue, Consts.SUBSCRIPTION_PLAN_PRICE.MaxValue))
            .LessThanOrEqualTo(Consts.SUBSCRIPTION_PLAN_PRICE.MaxValue)
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Price).ValueBetweenMessage(Consts.SUBSCRIPTION_PLAN_PRICE.MinValue, Consts.SUBSCRIPTION_PLAN_PRICE.MaxValue));

        RuleFor(f => f.Duration)
            .NotNull()
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Duration).IsRequiredMessage())
            .GreaterThanOrEqualTo(Consts.SUBSCRIPTION_PLAN_DURATION.MinValue)
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Duration).ValueBetweenMessage(Consts.SUBSCRIPTION_PLAN_DURATION.MinValue, Consts.SUBSCRIPTION_PLAN_DURATION.MaxValue))
            .LessThanOrEqualTo(Consts.SUBSCRIPTION_PLAN_DURATION.MaxValue)
            .WithMessage(nameof(UpdateSubscriptionPlanCommand.Duration).ValueBetweenMessage(Consts.SUBSCRIPTION_PLAN_DURATION.MinValue, Consts.SUBSCRIPTION_PLAN_DURATION.MaxValue));

    }
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
    
}