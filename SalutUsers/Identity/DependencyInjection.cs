using Identity.Models;
using Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var azureConfig = new AzureConfiguration();
        configuration.GetSection("AzureConfig").Bind(azureConfig);
        
        services.AddSingleton(azureConfig);

        services.AddScoped<IGraphServiceClientProvider, GraphServiceClientProvider>();

        return services;
    }
}
