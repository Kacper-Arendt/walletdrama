using Auth.Api;
using Shared.Infrastructure;
using Teams.Api;
using Teams.Api.Controllers;

namespace WalletDrama.Bootstrapper.DI;

public static class ModuleLoader
{
    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddInfrastructure();
        builder.RegisterAuthModule();
        builder.RegisterTeamsModule();

        builder.Services.AddControllers()
            .AddApplicationPart(typeof(TeamsController).Assembly);

        return builder;
    }

    public static WebApplication UseModules(this WebApplication app)
    {
        app.UseInfrastructure();
        app.UseAuthModule();
        app.UseTeamsModule();

        return app;
    }
}