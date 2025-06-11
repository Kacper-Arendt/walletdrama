using Budgets.Domain.ValueObjects;

namespace Budgets.Domain.Entities;

public class BudgetMonth
{
    public Month Month { get; init; }
    public BudgetId BudgetId { get; init; }

    public BudgetMonth(Month month, BudgetId budgetId)
    {
        Month = month;
        BudgetId = budgetId;
    }
}