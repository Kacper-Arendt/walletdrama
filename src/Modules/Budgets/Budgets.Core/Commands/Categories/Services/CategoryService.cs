using Budgets.Core.Commands.Categories.Dtos;
using Budgets.Core.Exceptions;
using Budgets.Domain.Factories;
using Budgets.Domain.ValueObjects;
using Budgets.Persistence;
using Shared.Infrastructure.Helpers;

namespace Budgets.Core.Commands.Categories.Services;

public class CategoryService : ICategoryService
{
    private readonly BudgetDbContext _budgetDbContext;
    private readonly HttpContextHelper _httpContextHelper;

    public CategoryService(BudgetDbContext budgetDbContext, HttpContextHelper httpContextHelper)
    {
        _budgetDbContext = budgetDbContext;
        _httpContextHelper = httpContextHelper;
    }

    public async Task<CategoryId> CreateAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
    {
        var user = _httpContextHelper.GetCurrentUser();
        var budget = _budgetDbContext.Budgets
            .FirstOrDefault(b => b.Id == createCategoryDto.BudgetId && b.OwnerId == user.Id);

        if (budget is null)
            throw new BudgetNotFoundException(createCategoryDto.BudgetId);

        var category = CategoryFactory.Create(
            new CategoryName(createCategoryDto.Name),
            createCategoryDto.Description,
            budget.Id,
            createCategoryDto.Type);

        _budgetDbContext.Categories.Add(category);
        await _budgetDbContext.SaveChangesAsync(cancellationToken);

        return category.Id;
    }

    public async Task UpdateAsync(UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken)
    {
        var user = _httpContextHelper.GetCurrentUser();
        var budget = _budgetDbContext.Budgets
            .FirstOrDefault(b => b.Id == updateCategoryDto.BudgetId && b.OwnerId == user.Id);

        if (budget is null)
            throw new BudgetNotFoundException(updateCategoryDto.BudgetId);

        var category = _budgetDbContext.Categories
            .FirstOrDefault(c => c.Id == updateCategoryDto.Id && c.BudgetId == updateCategoryDto.BudgetId);

        if (category is null)
            throw new CategoryNotFoundException(updateCategoryDto.Id);

        if (category.BudgetId != budget.Id)
            throw new BudgetNotFoundException(updateCategoryDto.BudgetId);

        category.Update(
            new CategoryName(updateCategoryDto.Name),
            updateCategoryDto.Description,
            updateCategoryDto.Type
        );

        _budgetDbContext.Categories.Update(category);
        await _budgetDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var user = _httpContextHelper.GetCurrentUser();
        var category = _budgetDbContext.Categories
            .FirstOrDefault(c => c.Id == categoryId);

        if (category is null)
            throw new CategoryNotFoundException(categoryId);

        var budget = _budgetDbContext.Budgets
            .FirstOrDefault(b => b.Id == category.BudgetId && b.OwnerId == user.Id);

        if (budget is null)
            throw new BudgetNotFoundException(category.BudgetId);

        category.Deactivate();
        
        _budgetDbContext.Categories.Update(category);
        await _budgetDbContext.SaveChangesAsync(cancellationToken);
    }
}