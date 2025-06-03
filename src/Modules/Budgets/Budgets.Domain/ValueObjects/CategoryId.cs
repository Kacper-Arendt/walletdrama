using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.ValueObjects;

public class CategoryId: ValueObject
{
    public Guid Value { get; }

    public CategoryId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("CategoryId cannot be empty.", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(CategoryId id) => id.Value;
    public static explicit operator CategoryId(Guid value) => new(value);
    
}