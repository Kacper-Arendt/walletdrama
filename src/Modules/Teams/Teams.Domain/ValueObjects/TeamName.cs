using Shared.Abstractions.Exceptions;
using Shared.Abstractions.ValueObjects;

namespace Teams.Domain.ValueObjects;

public class TeamName : ValueObject
{
    public string Value { get; }

    public TeamName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectInvalidTypeException("Team name cannot be empty.");

        if (value.Length > 100)
            throw new ValueObjectInvalidTypeException("Team name cannot exceed 100 characters.");

        Value = value;
    }

    public static implicit operator string(TeamName teamName) => teamName.Value;
    public static explicit operator TeamName(string value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}