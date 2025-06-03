using Budgets.Domain.Enums;
using Budgets.Domain.ValueObjects;

namespace Budgets.Domain.Entities;

public class Category
{
    public CategoryId Id { get; init; }
    public CategoryName Name { get; internal set; }
    public string Description { get; internal set; } = "";
    public BudgetId BudgetId { get; init; }
    public bool IsActive { get; internal set; } = true;
    public TransactionType Type { get; internal set; }
    
    private Category()
    {
    }
    
    public Category(CategoryId id, CategoryName name, string description, BudgetId budgetId, TransactionType type)
    {
        Id = id;
        Name = name;
        Description = description;
        BudgetId = budgetId;
        Type = type;
    }
    
    public void Update(CategoryName name, string description, TransactionType type)
    {
        Name = name;
        Description = description;
        Type = type;
    }
    
    public void Deactivate()
    {
        IsActive = false;
    }
}