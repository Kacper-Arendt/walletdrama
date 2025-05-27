namespace Budgets.Core.Queries.Budget.Dtos;

public record BudgetDetailsDto(Guid Id, Guid OwnerId, string Name, string Description)
{
    public static BudgetDetailsDto FromDomain(Domain.Entities.Budget budget)
    {
        return new BudgetDetailsDto(
            budget.Id.Value,
            budget.OwnerId,
            budget.Details.Name.Value,
            budget.Details.Description
        );
    }
}