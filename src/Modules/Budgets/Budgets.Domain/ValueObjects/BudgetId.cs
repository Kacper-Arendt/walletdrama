using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.ValueObjects;

public class BudgetId: ValueObject
{
    public Guid Value { get; }

    public BudgetId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("BudgetId cannot be empty.", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(BudgetId budgetId) => budgetId.Value;
    public static explicit operator BudgetId(Guid value) => new(value);
    
}