using Budgets.Domain.Entities;
using Budgets.Domain.Enums;
using Budgets.Domain.ValueObjects;
using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.Factories;

public static class BudgetYearFactory
{
    public static BudgetYear Create(YearId id, Year year, BudgetId budgetId)
    {
        return new BudgetYear(id, year, budgetId);
    }
}