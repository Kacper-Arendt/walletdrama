using Budgets.Domain.Entities;
using Budgets.Domain.Enums;

namespace Budgets.Core.Queries.Categories.Dtos;

public record CategoryDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public TransactionType Type { get; init; }
    public Guid BudgetId { get; init; }

    public static CategoryDto FromDomain(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id.Value,
            Name = category.Name.Value,
            Description = category.Description,
            IsActive = category.IsActive,
            Type = category.Type,
            BudgetId = category.BudgetId.Value
        };
    }
};