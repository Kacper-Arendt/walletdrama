using Budgets.Domain.Entities;
using Budgets.Domain.ValueObjects;
using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.Factories;

public static class BudgetFactory
{
    public static Budget Create(UserId ownerId, BudgetName name, string description)
    {
        var details = new BudgetDetails(name, description);

        return new Budget(new BudgetId(Guid.NewGuid()), ownerId, details);
    }
}