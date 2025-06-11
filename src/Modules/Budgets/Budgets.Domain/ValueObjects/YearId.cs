using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.ValueObjects;

public class YearId: ValueObject
{
    public Guid Value { get; }

    public YearId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("YearId cannot be empty.", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(YearId id) => id.Value;
    public static explicit operator YearId(Guid value) => new(value);
    
}