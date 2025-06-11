using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.ValueObjects;

public class Year: ValueObject
{
    public int Value { get; }

    public Year(int value)
    {
        if (value is < 2000 or > 2200)
            throw new ArgumentOutOfRangeException(nameof(value), "Year must be a valid year between 2000 and 2200.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(Year id) => id.Value;
    public static explicit operator Year(int value) => new(value);
    
}