using Auth.Core;
using Auth.Core.Database;
using Auth.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace __ModuleName__.Api;

public static class __ModuleName__Module
{
    public static WebApplicationBuilder Register__ModuleName__Module(this WebApplicationBuilder builder)
    {
        builder.Services.Add__ModuleName__Core(builder.Configuration); 
        
        return builder;
    }

    public static WebApplication Use__ModuleName__Module(this WebApplication app)
    {
        return app;
    }
}