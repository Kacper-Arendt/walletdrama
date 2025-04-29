using Auth.Core;
using Auth.Core.Database;
using Auth.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Api;

public static class UsersModule
{
    public static WebApplicationBuilder RegisterAuthModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthCore(builder.Configuration); 
        
        builder.Services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UsersEfContext>()
            .AddApiEndpoints();
        
        builder.Services.AddIdentityApiEndpoints<User>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);
            })
            .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<UsersEfContext>();
        
        return builder;
    }

    public static WebApplication UseAuthModule(this WebApplication app)
    {
        app
            .MapGroup("api/auth/identity")
            .MapIdentityApi<User>();

        return app;
    }
}