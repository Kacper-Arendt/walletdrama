using Budgets.Core.Queries.Budget.Dtos;

namespace Budgets.Core.Queries.Budget;

public interface IBudgetQueryService
{
    Task<BudgetDetailsDto> GetByIdAsync(Guid budgetId, CancellationToken cancellationToken = default);
    Task<IEnumerable<BudgetDto>> GetAllAsync(CancellationToken cancellationToken = default);
}