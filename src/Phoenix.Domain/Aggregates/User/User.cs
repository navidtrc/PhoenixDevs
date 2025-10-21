using Phoenix.Domain.Aggregates.Subscription;
using Phoenix.Domain.Aggregates.Subscription.Enums;
using Phoenix.Domain.Aggregates.User.Events;

namespace Phoenix.Domain.Aggregates.User;

public class User : AggregateRoot
{
    public string Username { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public SubscriptionPlan? CurrentPlan { get; private set; }
    public Ulid? CurrentPlanId { get; private set; }
    public SubscriptionPlan? ReservedPlan { get; private set; }
    public Ulid? ReservedPlanId { get; private set; }

    private User() { }

    public static User Create(string username, string email)
    {
        ValidateValues(
            username: username,
            email: email
        );

        var user = new User
        {
            Username = username.Trim(),
            Email = new Email(email.Trim()),
        };

        user.AddDomainEvent(new UserCreatedEvent(user));
        return user;
    }

    public void Update(
        string? username = null,
        string? email = null)
    {
        if (username is null && email is null)
            return;

        ValidateValues(
            username: username,
            email: email
        );

        var changed = false;

        if (username is not null)
        {
            var normalizedUsername = username.Trim();
            if (!string.Equals(Username, normalizedUsername, StringComparison.InvariantCulture))
            {
                Username = normalizedUsername;
                changed = true;
            }
        }

        if (email is not null)
        {
            var normalizedEmail = email.Trim();
            if (!string.Equals(Email.Value, normalizedEmail, StringComparison.InvariantCulture))
            {
                Email = new Email(normalizedEmail);
                changed = true;
            }
        }

        if (changed)
        {
            AddDomainEvent(new UserUpdatedEvent(this));
        }
    }

    public void UpdatePlan(Ulid planId)
    {
        CurrentPlanId = planId;
        if (CurrentPlan == null && ReservedPlan != null)
        {
            ActivateReservedPlan();
        }

        AddDomainEvent(new UserPlanUpdatedEvent(this));
    }

    public void SetReservedPlan(SubscriptionPlan reservedPlan)
    {
        ReservedPlan = reservedPlan;
        ReservedPlanId = reservedPlan.Id;
        AddDomainEvent(new UserReservedPlanUpdatedEvent(this));
    }

    public void ActivateReservedPlan()
    {
        if (ReservedPlan == null)
        {
            throw new InvalidOperationException("No reserved plan available to activate.");
        }

        CurrentPlan = ReservedPlan;
        CurrentPlanId = ReservedPlanId;
        ReservedPlan = null;
        ReservedPlanId = null;
        AddDomainEvent(new UserReservedPlanActivatedEvent(this));
    }

    public void RemoveCurrentPlan()
    {
        CurrentPlanId = null;
        CurrentPlan = null;
    }
    
    public void ClearReservedPlan()
    {
        ReservedPlanId = null;
        ReservedPlan = null;
    }

    private static void ValidateValues(
        string? username = null,
        string? email = null)
    {
        if (username is not null)
        {
            Guard.Against.NullOrWhiteSpace(
                username,
                nameof(username),
                exceptionCreator: () => new RequiredException(nameof(username)));

            Guard.Against.LengthOutOfRange(
                username,
                Consts.USER_USERNAME.MinLength,
                Consts.USER_USERNAME.MaxLength,
                exceptionCreator: () => new StringLengthBetweenException(
                    nameof(username),
                    Consts.USER_USERNAME.MinLength,
                    Consts.USER_USERNAME.MaxLength));
        }

        if (email is not null)
        {
            Guard.Against.NullOrWhiteSpace(
                email,
                nameof(email),
                exceptionCreator: () => new RequiredException(nameof(email)));

            if (!email.IsValidEmail())
            {
                throw new NotValidException(nameof(email));
            }

            Guard.Against.LengthOutOfRange(
                email,
                Consts.EMAIL.MinLength,
                Consts.EMAIL.MaxLength,
                exceptionCreator: () => new StringLengthBetweenException(
                    nameof(email),
                    Consts.EMAIL.MinLength,
                    Consts.EMAIL.MaxLength));
        }
    }
}