using Budgets.Core.Commands.BudgetManagement.Dtos;
using Budgets.Domain.ValueObjects;

namespace Budgets.Core.Commands.BudgetManagement.Services;

public interface IBudgetManagement
{
    Task<BudgetId> CreateAsync(CreateBudgetDto createBudgetDto, CancellationToken cancellationToken);
    Task UpdateDetailsAsync(UpdateBudgetDetailsDto detailsDto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid budgetId, CancellationToken cancellationToken);
}