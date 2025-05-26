using Budgets.Core.Commands.BudgetManagement.Services;
using Budgets.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Budgets.Core;

public static class Extensions
{
    public static IServiceCollection AddBudgetsCore(this IServiceCollection services)
    {
        services.AddBudgetsPersistence();

        services.AddScoped<IBudgetManagement, BudgetManagementService>();

        return services;
    }
}