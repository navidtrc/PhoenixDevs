using FluentValidation;
using Utilities.Resources;

namespace Phoenix.Application.User.Commands.SetUserReservedPlan;

public class SetUserReservedPlanCommandValidator: AbstractValidator<SetUserReservedPlanCommand>
{
    public SetUserReservedPlanCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage(nameof(SetUserReservedPlanCommand.UserId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(SetUserReservedPlanCommand.UserId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(SetUserReservedPlanCommand.UserId).FormatIncorrectMessage());
        
        RuleFor(x => x.ReservedPlanId)
            .NotNull()
            .WithMessage(nameof(SetUserReservedPlanCommand.ReservedPlanId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(SetUserReservedPlanCommand.ReservedPlanId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(SetUserReservedPlanCommand.ReservedPlanId).FormatIncorrectMessage());
    }
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
    
}