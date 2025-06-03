using Budgets.Domain.ValueObjects;
using Shared.Abstractions.ValueObjects;

namespace Budgets.Domain.Entities;

public class Budget
{
    public BudgetId Id { get; init; }
    public UserId OwnerId { get; init; }

    public BudgetDetails Details { get; init; }
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();

    private Budget()
    {
    }

    public Budget(BudgetId id, UserId ownerId, BudgetDetails details)
    {
        Id = id;
        OwnerId = ownerId;
        Details = details;
    }

    public void UpdateDetails(BudgetName name, string description)
    {
        Details.Name = name;
        Details.Description = description;
    }
}