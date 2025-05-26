using Budgets.Core.Commands.BudgetManagement.Dtos;
using Budgets.Core.Exceptions;
using Budgets.Domain.Factories;
using Budgets.Domain.ValueObjects;
using Budgets.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Helpers;

namespace Budgets.Core.Commands.BudgetManagement.Services;

public class BudgetManagementService(BudgetDbContext budgetDbContext, HttpContextHelper httpContextHelper)
    : IBudgetManagement
{
    public async Task<BudgetId> CreateAsync(CreateBudgetDto createBudgetDto, CancellationToken cancellationToken)
    {
        var user = httpContextHelper.GetCurrentUser();
        var budget = BudgetFactory.Create(user.Id, new BudgetName(createBudgetDto.Name), createBudgetDto.Description);

        await budgetDbContext.Budgets.AddAsync(budget, cancellationToken);
        await budgetDbContext.SaveChangesAsync(cancellationToken);

        return budget.Id;
    }

    public async Task UpdateDetailsAsync(UpdateBudgetDetailsDto detailsDto, CancellationToken cancellationToken)
    {
        var user = httpContextHelper.GetCurrentUser();

        var budget = await budgetDbContext.Budgets
            .FirstOrDefaultAsync(b => b.Id == detailsDto.Id && b.OwnerId == user.Id, cancellationToken);

        if (budget == null)
        {
            throw new BudgetNotFoundException(detailsDto.Id);
        }

        budget.UpdateDetails(new BudgetName(detailsDto.Name), detailsDto.Description);
        budgetDbContext.Budgets.Update(budget);
        await budgetDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid budgetId, CancellationToken cancellationToken)
    {
        var user = httpContextHelper.GetCurrentUser();

        var budget = await budgetDbContext.Budgets
            .FirstOrDefaultAsync(b => b.Id == new BudgetId(budgetId) && b.OwnerId == user.Id, cancellationToken);

        if (budget == null)
        {
            throw new BudgetNotFoundException(budgetId);
        }

        budgetDbContext.Budgets.Remove(budget);
        await budgetDbContext.SaveChangesAsync(cancellationToken);
    }
}