using Auth.Api;
using Shared.Infrastructure;
using Teams.Api;

namespace WalletDrama.Bootstrapper.DI;

public static class ModuleLoader
{
    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder) {
        builder.Services.AddInfrastructure();
        builder.RegisterAuthModule();
        builder.RegisterTeamsModule();

        builder.Services.AddControllers();
            // .AddApplicationPart(typeof(LanguagesController).Assembly);

        return builder;
    }
    
    public static WebApplication UseModules(this WebApplication app) {
        app.UseInfrastructure();
        app.UseAuthModule();
        app.UseTeamsModule();
        
        return app;
    }
}