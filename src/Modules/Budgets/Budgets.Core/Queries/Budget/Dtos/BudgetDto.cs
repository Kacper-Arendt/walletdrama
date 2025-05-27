namespace Budgets.Core.Queries.Budget.Dtos;

public record BudgetDto(Guid Id, string Name, string Description)
{
    public static BudgetDto FromDomain(Budgets.Domain.Entities.Budget budget)
    {
        return new BudgetDto(
            budget.Id.Value,
            budget.Details.Name.Value,
            budget.Details.Description
        );
    }
}