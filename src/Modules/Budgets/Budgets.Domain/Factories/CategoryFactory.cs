using Budgets.Domain.Entities;
using Budgets.Domain.Enums;
using Budgets.Domain.ValueObjects;
using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.Factories;

public static class CategoryFactory
{
    public static Category Create(CategoryName name, string description, BudgetId budgetId, TransactionType type)
    {
        return new Category(new CategoryId(Guid.NewGuid()), name, description, budgetId, type);
    }
}