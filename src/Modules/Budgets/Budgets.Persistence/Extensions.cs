using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Factories;
using Shared.Infrastructure.SqlServer;

namespace Budgets.Persistence;

public static class Extensions
{
    public static IServiceCollection AddBudgetsPersistence(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var dbConnectionStringFactory = serviceProvider.GetRequiredService<IDbConnectionStringFactory>();
        var connectionString = dbConnectionStringFactory.GetConnectionString(DatabaseType.DefaultConnection);
        services.AddSqlServerWithEfCore<BudgetDbContext>(connectionString);

        return services;
    }
}