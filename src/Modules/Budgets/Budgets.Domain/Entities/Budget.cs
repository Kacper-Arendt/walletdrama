using Budgets.Domain.ValueObjects;

namespace Budgets.Domain.Entities;

public class Budget
{
    public BudgetId Id { get; set; }
    public BudgetName Name { get; set; }
}