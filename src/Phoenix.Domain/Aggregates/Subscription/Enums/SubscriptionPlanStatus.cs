namespace Phoenix.Domain.Aggregates.Subscription.Enums;

public enum SubscriptionPlanStatus
{
    /// <summary>
    /// The subscription plan is currently active and usable.
    /// </summary>
    Active = 1,

    /// <summary>
    /// The subscription plan is temporarily disabled or expired.
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// The subscription plan is created but not yet active (e.g., awaiting payment or verification).
    /// </summary>
    Pending = 3,

    /// <summary>
    /// The subscription plan is not yet ready for use (e.g., under setup or pre-launch).
    /// </summary>
    NotReady = 4
    
}