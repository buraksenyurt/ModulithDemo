using System.Reflection;
using Carter;
using Microsoft.Extensions.DependencyInjection;

namespace RentAGame.Shared.Extensions;

/*
    Çözümde birçok modüle yer alacak ve bu modüllerde ICarterModule'ten türemiş birçok Endpoint olacak.
    Bu endpoint'ler için gerekli Carter servisilerinin register işlemleri için ilgili assembly'ların
    ICarterModule türevli türlerini yakalayıp Carter ile ilişkilendirmek gerekiyor.

    Aşağıdaki genişletme metodu ilgili modüllerin çalışma zamanındaki DI Container'a kolayca eklenmesini sağlamak için geliştirilmiş.
*/

public static class CarterDependencyInjection
{
    public static IServiceCollection AddCarterModulesFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddCarter(configurator: config =>
        {
            foreach (var assembly in assemblies)
            {
                var modules = assembly
                            .GetTypes()
                            .Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();

                config.WithModules(modules);
            }
        });

        return services;
    }
}