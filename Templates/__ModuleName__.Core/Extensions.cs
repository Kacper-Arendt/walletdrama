using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace __ModuleName__.Core;

public static class Extensions
{
    public static IServiceCollection Add__ModuleName__Core(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }    
}