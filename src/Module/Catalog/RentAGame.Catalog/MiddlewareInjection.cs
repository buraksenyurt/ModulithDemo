using Microsoft.AspNetCore.Builder;

namespace RentAGame.Catalog;

public static class MiddlewareInjection
{
    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        return app;
    }
}