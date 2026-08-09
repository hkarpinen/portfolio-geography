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

        services.AddHttpClient<IWeatherQuery, OpenWeatherMapClient>();
        services.AddHttpClient<ITileProxy, TileProxyClient>();

        return services;
    }
}
