using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.ValueObjects;

public class Month: ValueObject
{
    public int Value { get; }

    public Month(int value)
    {
        if (value is < 1 or > 12)
            throw new ArgumentOutOfRangeException(nameof(value), "Month must be a valid month between 1 and 12.");
        
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(Month id) => id.Value;
    public static explicit operator Month(int value) => new(value);
    
}