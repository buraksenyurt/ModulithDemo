using Microsoft.AspNetCore.Builder;
using RentAGame.Shared.Data;

namespace RentAGame.Catalog;

public static class MiddlewareInjection
{
    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        app.UseMigration<CatalogDbContext>();
        return app;
    }
}