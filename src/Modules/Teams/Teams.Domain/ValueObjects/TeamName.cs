using Shared.Abstractions.ValueObjects;

namespace Teams.Domain.ValueObjects;

public class TeamName : ValueObject
{
    public string Value { get; }

    public TeamName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Team name cannot be empty.", nameof(value));

        if (value.Length > 100)
            throw new ArgumentException("Team name cannot exceed 100 characters.", nameof(value));

        Value = value;
    }

    public static implicit operator string(TeamName teamName) => teamName.Value;
    public static explicit operator TeamName(string value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}