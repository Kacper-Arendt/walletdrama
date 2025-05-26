using Auth.Api;
using Budgets.Api;
using Budgets.Api.Controllers;
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
        builder.RegisterBudgetsModule();
        builder.RegisterTeamsModule();

        builder.Services.AddControllers()
            .AddApplicationPart(typeof(BudgetBaseController).Assembly)
            .AddApplicationPart(typeof(TeamsController).Assembly);

        return builder;
    }

    public static WebApplication UseModules(this WebApplication app)
    {
        app.UseInfrastructure();
        app.UseAuthModule();
        app.UseBudgetsModule();
        app.UseTeamsModule();

        return app;
    }
}