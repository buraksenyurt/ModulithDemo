using Microsoft.AspNetCore.Builder;

namespace RentAGame.Ordering;

public static class MiddlewareInjection
{
    public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
    {
        return app;
    }
}