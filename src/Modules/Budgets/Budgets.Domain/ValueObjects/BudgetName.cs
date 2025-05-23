using Shared.Abstractions.Exceptions;
using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.ValueObjects;

public class BudgetName : ValueObject
{
    public string Value { get; }

    public BudgetName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("BudgetName cannot be empty.", nameof(value));
        
        if (value.Length > 100)
            throw new ValueObjectInvalidTypeException("Budget name cannot exceed 100 characters.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(BudgetName name) => name.Value;
    public static explicit operator BudgetName(string value) => new(value);
    
}