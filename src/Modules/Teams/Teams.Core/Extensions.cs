using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Teams.Core;

public static class Extensions
{
    public static IServiceCollection AddTeamsCore(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }    
}