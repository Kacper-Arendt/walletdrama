using Shared.Abstractions.ValueObjects;

namespace Teams.Domain.ValueObjects;

public class TeamId : ValueObject
{
    public Guid Value { get; }

    public TeamId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("TeamId cannot be empty.", nameof(value));

        Value = value;
    }


    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();

    public static implicit operator Guid(TeamId userId) => userId.Value;
    public static explicit operator TeamId(Guid value) => new(value);
}