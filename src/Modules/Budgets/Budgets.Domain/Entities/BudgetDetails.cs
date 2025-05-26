using Budgets.Domain.ValueObjects;

namespace Budgets.Domain.Entities;

public class BudgetDetails
{
    public BudgetName Name { get; internal set; }
    public string Description { get; internal  set; }
}