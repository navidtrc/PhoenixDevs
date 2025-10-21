using Framework.Core.Domain.ValueObjects;

namespace Framework.Core.Domain.Abstractions;

public abstract class BaseEntity<TId> : IAuditableEntity
    where TId : struct,
    IComparable,
    IComparable<TId>,
    IEquatable<TId>,
    IFormattable
{
    public TId Id { get; protected set; }
    public DateTimeOffset CreatedOn { get; protected set; } = DateTimeOffset.Now;
    public DateTimeOffset? LastUpdatedOn { get; protected set; }
    public bool IsArchived { get; protected set; } = false;

    protected BaseEntity() { }

    private bool Equals(BaseEntity<TId>? other) => this == other;
    public override bool Equals(object? obj) =>
        obj is BaseEntity<TId> otherObject && Id.Equals(otherObject.Id);

    public override int GetHashCode() => Id.GetHashCode();
    public static bool operator ==(BaseEntity<TId> left, BaseEntity<TId> right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
        => !(right == left);
}

public abstract class Entity : BaseEntity<Ulid>;