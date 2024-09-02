using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RentAGame.Catalog;


public static class DependencyInjection
{
    /*
        Runtime DI servislerine Catalog modülü ile ilgili servislerin kayıt ediliği genişletme metodu.

        #0 
        
        Npgsql paketini kullanarak ve runtime appsettings dosyasından DbConStr değerini alarak Postgresql veritabanına
        bağlanabilen bir EF bileşenini DI servislerine kayıt ediyoruz.
    */
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString("DbConStr");
        services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connStr));
        return services;
    }
}
