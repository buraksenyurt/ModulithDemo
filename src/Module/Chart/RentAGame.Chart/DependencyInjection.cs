using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RentAGame.Chart;

public static class DependencyInjection
{
    public static IServiceCollection AddChartModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
