using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.SubscriptionPlan.Commands.Create;

public class CreateSubscriptionPlanCommandHandler(ApplicationDbContext context) 
    : IRequestHandler<CreateSubscriptionPlanCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = Domain.Aggregates.Subscription.SubscriptionPlan.Create(request.Title,
            request.Description,
            request.Price,
            request.Duration);
        await context.SubscriptionPlans.AddAsync(subscriptionPlan, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}