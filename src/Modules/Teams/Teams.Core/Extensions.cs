using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Teams.Core.Authorization;
using Teams.Core.Commands;
using Teams.Core.Queries;
using Teams.Persistence;

namespace Teams.Core;

public static class Extensions
{
    public static IServiceCollection AddTeamsCore(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("IsTeamOwner", policy =>
            {
                policy.Requirements.Add(new IsTeamOwnerRequirement()); 
                
            });

        services.AddTeamsPersistence();
        services.AddScoped<ITeamsCommandService, TeamsCommandService>();
        services.AddScoped<ITeamsQueryService, TeamsQueryService>();
        services.AddScoped<IAuthorizationHandler, IsTeamOwnerHandler>();

        return services;
    }
}