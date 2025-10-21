using FluentValidation;
using Phoenix.Domain;
using Utilities.Resources;

namespace Phoenix.Application.SubscriptionPlan.Commands.Create;

public class CreateSubscriptionPlanCommandValidator: AbstractValidator<CreateSubscriptionPlanCommand>
{
    public CreateSubscriptionPlanCommandValidator()
    {
        RuleFor(f => f.Title)
            .NotNull()
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Title).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Title).IsRequiredMessage())
            .MaximumLength(Consts.SUBSCRIPTION_PLAN_TITLE.MaxLength)
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Title).StringLengthBetweenMessage(Consts.SUBSCRIPTION_PLAN_TITLE.MinLength, Consts.SUBSCRIPTION_PLAN_TITLE.MaxLength))
            .MinimumLength(Consts.SUBSCRIPTION_PLAN_TITLE.MinLength)
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Title).StringLengthBetweenMessage(Consts.SUBSCRIPTION_PLAN_TITLE.MinLength, Consts.SUBSCRIPTION_PLAN_TITLE.MaxLength));

        RuleFor(f => f.Description)
            .NotNull()
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Description).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Description).IsRequiredMessage())
            .MaximumLength(Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MaxLength)
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Description).StringLengthBetweenMessage(Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MinLength, Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MaxLength))
            .MinimumLength(Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MinLength)
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Description).StringLengthBetweenMessage(Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MinLength, Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MaxLength));

        RuleFor(f => f.Price)
            .NotNull()
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Price).IsRequiredMessage())
            .GreaterThanOrEqualTo(Consts.SUBSCRIPTION_PLAN_PRICE.MinValue)
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Price).ValueBetweenMessage(Consts.SUBSCRIPTION_PLAN_PRICE.MinValue, Consts.SUBSCRIPTION_PLAN_PRICE.MaxValue))
            .LessThanOrEqualTo(Consts.SUBSCRIPTION_PLAN_PRICE.MaxValue)
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Price).ValueBetweenMessage(Consts.SUBSCRIPTION_PLAN_PRICE.MinValue, Consts.SUBSCRIPTION_PLAN_PRICE.MaxValue));

        RuleFor(f => f.Duration)
            .NotNull()
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Duration).IsRequiredMessage())
            .GreaterThanOrEqualTo(Consts.SUBSCRIPTION_PLAN_DURATION.MinValue)
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Duration).ValueBetweenMessage(Consts.SUBSCRIPTION_PLAN_DURATION.MinValue, Consts.SUBSCRIPTION_PLAN_DURATION.MaxValue))
            .LessThanOrEqualTo(Consts.SUBSCRIPTION_PLAN_DURATION.MaxValue)
            .WithMessage(nameof(CreateSubscriptionPlanCommand.Duration).ValueBetweenMessage(Consts.SUBSCRIPTION_PLAN_DURATION.MinValue, Consts.SUBSCRIPTION_PLAN_DURATION.MaxValue));

    }
    
}