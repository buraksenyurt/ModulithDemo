using Microsoft.EntityFrameworkCore.Diagnostics;
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

        #1
        
        Ayrıca seed operasyonları(yani eğer veritabanında veri yoksa örnek verilerin eklenmesi adımı) için
        IDataSeet bileşenini enjekte ediyoruz.

        #2 
        
        Bununla birlikte Meditor ve Entity Interceptor servislerini de DI katmanına bildiriyoruz.
    */
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        /*
            Aggregation nesnelerinde vuku bulan olayların Meditar'a gönderilmesi söz konusu.
            Bunun için DomainEventInterceptor kullanılıyor. Bu bileşen ise Mediator bileşenine bağımlı.
            Dolayısıyla DI servislerine mediator bileşeninin bildirilmesi lazım.
        */
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        // Assembly üzerindeki Fluent Validator'lar DI servislerine kayıt edilir
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        var connStr = configuration.GetConnectionString("DbConStr");

        services.AddScoped<ISaveChangesInterceptor, EntityUpsertInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DomainEventInterceptor>();

        services.AddDbContext<CatalogDbContext>((serviceProvider, options) =>
        {
            // Postgresql kullanacak bileşen bildirimi
            options.UseNpgsql(connStr);
            // Entity ekleme ve güncelleme işlerinde araya girecek Interceptor nesnelerini yüklüyoruz.
            // Yukarıdaki Injection bildirimine göre EntityUpsertInterceptor ile DomainEventInterceptor
            // bileşenleri yüklenecek.
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });
        services.AddScoped<IDataSeed, CatalogDataSeeder>();
        return services;
    }
}
