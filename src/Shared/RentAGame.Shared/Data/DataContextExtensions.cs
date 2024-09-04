using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RentAGame.Shared.Data;

public static class DataContextExtensions
{
    public static IApplicationBuilder UseMigration<T>(this IApplicationBuilder app) where T : DbContext
    {
        /* 
            EF Migration işlemlerini otomatik olarak başlatmak için eklenen kısımdır.            
            Böylece sürekli komut satırından migration planları takip etmek, çalıştırmak zorunda kalmayız.
            Ayrıca seed operasyonu da burada yapılabilir.
         */
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<T>();
        dbContext?.Database.MigrateAsync().Wait();

        // Seed operasyonları      
        var seedOperators = scope.ServiceProvider.GetServices<IDataSeed>();
        foreach (var seedOperator in seedOperators)
        {
            seedOperator.SeedAllAsync().Wait();
        }
        return app;
    }
}