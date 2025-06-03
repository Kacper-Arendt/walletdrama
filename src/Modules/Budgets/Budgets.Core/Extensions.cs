using Budgets.Core.Commands.BudgetManagement.Services;
using Budgets.Core.Commands.Categories.Services;
using Budgets.Core.Queries.Budget;
using Budgets.Core.Queries.Categories;
using Budgets.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Budgets.Core;

public static class Extensions
{
    public static IServiceCollection AddBudgetsCore(this IServiceCollection services)
    {
        services.AddBudgetsPersistence();

        services.AddScoped<IBudgetManagement, BudgetManagementService>();
        services.AddScoped<IBudgetQueryService, BudgetQueryService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryQueryService, CategoryQueryService>();

        return services;
    }
}