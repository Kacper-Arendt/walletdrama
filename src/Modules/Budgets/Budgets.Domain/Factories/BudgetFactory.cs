using Budgets.Domain.Entities;
using Budgets.Domain.ValueObjects;
using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.Factories;

public static class BudgetFactory
{
    public static Budget Create(UserId ownerId, BudgetName name, string description)
    {
        var details = new BudgetDetails
        {
            Name = name,
            Description = description
        };

        return new Budget(ownerId, details);
    }
}