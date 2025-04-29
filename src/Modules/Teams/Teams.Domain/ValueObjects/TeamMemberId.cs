using Shared.Abstractions.ValueObjects;

namespace Teams.Domain.ValueObjects;

public class TeamMemberId : ValueObject
{
    public Guid Value { get; }

    public TeamMemberId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("UserTeamId cannot be empty.", nameof(value));

        Value = value;
    }


    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();

    public static implicit operator Guid(TeamMemberId memberId) => memberId.Value;
    public static explicit operator TeamMemberId(Guid value) => new(value);
}