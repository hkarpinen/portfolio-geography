using Geography.Application.Queries;
using Geography.Infrastructure.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geography.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OpenWeatherMapOptions>(
            configuration.GetSection("OpenWeatherMap"));

        // Typed HttpClient — DI injects IOptions<OpenWeatherMapOptions> automatically.
        services.AddHttpClient<IWeatherQuery, OpenWeatherMapClient>();
        services.AddHttpClient<ITileProxy, TileProxyClient>();

        return services;
    }
}
