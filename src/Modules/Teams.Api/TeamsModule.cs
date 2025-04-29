using Microsoft.AspNetCore.Builder;
using Teams.Core;
using Teams.Persistence;

namespace Teams.Api;

public static class TeamsModule
{
    public static WebApplicationBuilder RegisterTeamsModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddTeamsPersistence();
        builder.Services.AddTeamsCore();

        return builder;
    }

    public static WebApplication UseTeamsModule(this WebApplication app)
    {
        return app;
    }
}