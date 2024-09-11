using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RentAGame.ShoppingCart;

public static class DependencyInjection
{
    public static IServiceCollection AddShoppingCartModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
