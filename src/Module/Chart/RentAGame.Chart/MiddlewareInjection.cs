using Microsoft.AspNetCore.Builder;

namespace RentAGame.Chart;

public static class MiddlewareInjection
{
    public static IApplicationBuilder UseChartModule(this IApplicationBuilder app)
    {
        return app;
    }
}