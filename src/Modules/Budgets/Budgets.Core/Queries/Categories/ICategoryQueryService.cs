using Budgets.Core.Queries.Categories.Dtos;

namespace Budgets.Core.Queries.Categories;

public interface ICategoryQueryService
{
    Task<IEnumerable<CategoryDto>> GetByBudgetIdAsync(Guid budgetId, CancellationToken cancellationToken = default);
}