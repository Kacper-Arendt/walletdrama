using Budgets.Core.Exceptions;
using Budgets.Core.Queries.Categories.Dtos;
using Budgets.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Helpers;

namespace Budgets.Core.Queries.Categories;

public class CategoryQueryService(BudgetDbContext budgetDbContext, HttpContextHelper httpContextHelper)
    : ICategoryQueryService
{
    public async Task<IEnumerable<CategoryDto>> GetByBudgetIdAsync(Guid budgetId, CancellationToken cancellationToken = default)
    {
        var currentUser = httpContextHelper.GetCurrentUser();
        
        var budget = await budgetDbContext.Budgets
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == budgetId && b.OwnerId == currentUser.Id, cancellationToken);
        
        if (budget is null)
            throw new BudgetNotFoundException(budgetId);
        
        var categories = await budgetDbContext.Categories
            .AsNoTracking()
            .Where(c => c.BudgetId == budgetId)
            .ToListAsync(cancellationToken);
        
        return categories.Select(CategoryDto.FromDomain);
    }
}