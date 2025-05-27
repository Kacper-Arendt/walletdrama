using Budgets.Core.Exceptions;
using Budgets.Core.Queries.Budget.Dtos;
using Budgets.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Helpers;

namespace Budgets.Core.Queries.Budget;

public class BudgetQueryService(BudgetDbContext budgetDbContext, HttpContextHelper httpContextHelper)
    : IBudgetQueryService
{
    public async Task<BudgetDetailsDto> GetByIdAsync(Guid budgetId, CancellationToken cancellationToken = default)
    {
        var currentUser = httpContextHelper.GetCurrentUser();
        var budget = await budgetDbContext.Budgets
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == budgetId && b.OwnerId == currentUser.Id, cancellationToken);

        if (budget is null)
            throw new BudgetNotFoundException(budgetId);

        return BudgetDetailsDto.FromDomain(budget);
    }

    public async Task<IEnumerable<BudgetDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var currentUser = httpContextHelper.GetCurrentUser();
        var budgets = await budgetDbContext.Budgets
            .AsNoTracking()
            .Where(b => b.OwnerId == currentUser.Id)
            .ToListAsync(cancellationToken);

        return budgets.Select(BudgetDto.FromDomain);
    }
}