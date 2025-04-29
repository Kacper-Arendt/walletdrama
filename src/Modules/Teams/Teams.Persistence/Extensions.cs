using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Factories;
using Shared.Infrastructure.SqlServer;

namespace Teams.Persistence;

public static class Extensions
{
    public static IServiceCollection AddTeamsPersistence(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var dbConnectionStringFactory = serviceProvider.GetRequiredService<IDbConnectionStringFactory>();
        var connectionString = dbConnectionStringFactory.GetConnectionString(DatabaseType.DefaultConnection);
        services.AddSqlServerWithEfCore<TeamsDbContext>(connectionString);

        return services;
    }
}