using FluentValidation;
using Utilities.Resources;

namespace Phoenix.Application.User.Commands.UpdateUserPlan;

public class UpdateUserPlanCommandValidator: AbstractValidator<UpdateUserPlanCommand>
{
    public UpdateUserPlanCommandValidator()
    {
        RuleFor(x => x.PlanId)
            .NotNull()
            .WithMessage(nameof(UpdateUserPlanCommand.PlanId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(UpdateUserPlanCommand.PlanId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(UpdateUserPlanCommand.PlanId).FormatIncorrectMessage());
        
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage(nameof(UpdateUserPlanCommand.UserId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(UpdateUserPlanCommand.UserId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(UpdateUserPlanCommand.UserId).FormatIncorrectMessage());
    }
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
}