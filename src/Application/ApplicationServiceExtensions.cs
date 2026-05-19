using Microsoft.Extensions.DependencyInjection;

namespace Geography.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
