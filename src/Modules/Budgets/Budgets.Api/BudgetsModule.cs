using Budgets.Core;
using Microsoft.AspNetCore.Builder;

namespace Budgets.Api;

public static class BudgetsModule
{
    public static WebApplicationBuilder RegisterBudgetsModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddBudgetsCore();

        return builder;
    }

    public static WebApplication UseBudgetsModule(this WebApplication app)
    {
        return app;
    }
}