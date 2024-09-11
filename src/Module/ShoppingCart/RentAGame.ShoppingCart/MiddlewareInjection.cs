using Microsoft.AspNetCore.Builder;

namespace RentAGame.ShoppingCart;

public static class MiddlewareInjection
{
    public static IApplicationBuilder UseShoppingCartModule(this IApplicationBuilder app)
    {
        return app;
    }
}