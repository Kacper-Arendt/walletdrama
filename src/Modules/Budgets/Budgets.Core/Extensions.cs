using Budgets.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Budgets.Core;

public static class Extensions
{
    public static IServiceCollection AddBudgetsCore(this IServiceCollection services)
    {
        services.AddBudgetsPersistence();

        return services;
    }
}