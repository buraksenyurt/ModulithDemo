using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RentAGame.Catalog.Data.Interceptors;

/*
    Bu interceptor bileşenini Domain event'lerini yakalamak ve her bir olay için
    Mediator üzerinden bir komut çalıştırmak için kullanıyoruz.

    Olayları kaydetme işlemi esnasında yakalamamız gerektiğinden SavingChanges ve
    SavingChanges metotları yeniden yazılıyor.

    Meditor bağımlılığını ise Primary Constructor üzerinden enjekte ediyoruz.
*/
public class DomainEventInterceptor(IMediator mediator)
    : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvent(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvent(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvent(DbContext? dbContext)
    {
        // Ortada bir DbContext yoksa çık
        if (dbContext == null)
            return;

        // Önce Event listesinde herhangibir içerik olan IAggregate türevli Entity nesneleri bulunur
        var aggregates = dbContext.ChangeTracker.Entries<IAggregate>().Where(a => a.Entity.Events.Any()).Select(a => a.Entity);
        // Bu nesnelere yüklenmiş olan IEvent türevleri çekilir
        var events = aggregates.SelectMany(a => a.Events).ToList();

        aggregates.ToList().ForEach(a => a.ClearEvents());

        // Herbir Event nesnesi mediator'a gönderilir.
        foreach (var e in events)
        {
            await mediator.Publish(e);
        }
    }
}