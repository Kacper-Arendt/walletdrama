using Auth.Core;
using Auth.Core.Database;
using Auth.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Teams.Api;

public static class TeamsModule
{
    public static WebApplicationBuilder RegisterTeamsModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddMyModuleCore(builder.Configuration); 
        
        return builder;
    }

    public static WebApplication UseTeamsModule(this WebApplication app)
    {
        return app;
    }
}