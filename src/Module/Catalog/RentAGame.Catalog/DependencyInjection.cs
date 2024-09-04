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

        Ayrıca seed operasyonları(yani eğer veritabanında veri yoksa örnek verilerin eklenmesi adımı) için
        IDataSeet bileşenini enjekte ediyoruz.
    */
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString("DbConStr");
        services.AddDbContext<CatalogDbContext>(options =>
        {
            // Postgresql kullanacak bileşen bildirimi
            options.UseNpgsql(connStr);
            // Entity ekleme ve güncelleme işlerinde araya girecek Interceptor bileşeni bildirimi
            options.AddInterceptors(new EntityUpsertInterceptor());
        });
        services.AddScoped<IDataSeed, CatalogDataSeeder>();
        return services;
    }
}
