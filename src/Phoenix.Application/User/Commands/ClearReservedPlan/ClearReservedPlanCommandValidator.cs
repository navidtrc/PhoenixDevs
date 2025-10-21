using FluentValidation;
using Utilities.Resources;

namespace Phoenix.Application.User.Commands.ClearReservedPlan;

public class ClearReservedPlanCommandValidator: AbstractValidator<ClearReservedPlanCommand>
{
    public ClearReservedPlanCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage(nameof(ClearReservedPlanCommand.UserId).IsRequiredMessage())
            .NotEmpty()
            .WithMessage(nameof(ClearReservedPlanCommand.UserId).IsRequiredMessage())
            .Must(BeValidUlid)
            .WithMessage(nameof(ClearReservedPlanCommand.UserId).FormatIncorrectMessage());
    }
    private bool BeValidUlid(Ulid id) => Ulid.TryParse(id.ToString(), out _);
    
}