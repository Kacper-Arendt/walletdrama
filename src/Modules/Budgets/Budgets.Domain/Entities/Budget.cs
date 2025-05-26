using Budgets.Domain.ValueObjects;
using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.Entities;

public class Budget
{
    public BudgetId Id { get; private set; }
    public UserId OwnerId { get; }

    public BudgetDetails Details { get; }

    public Budget(UserId ownerId, BudgetDetails details)
    {
        OwnerId = ownerId;
        Details = details;
    }

    public void UpdateDetails(BudgetName name, string description)
    {
        Details.Name = name;
        Details.Description = description;
    }
}