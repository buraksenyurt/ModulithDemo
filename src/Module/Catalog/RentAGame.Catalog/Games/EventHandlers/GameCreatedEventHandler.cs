namespace RentAGame.Catalog.Games.EventHandlers;

/*
    Yeni bir oyun nesnesi oluşturulduğunda bununla ilgili Aggregation nesnesi 
    GameCreateEvent isimli bir olay yayınlar. Bu nesne MediatR'dan gelen INotification implementasyonudur.
    Aşağıdaki Handler nesnesi yine MediatR'dan gelen INotificationHandler<T> arayüzünü, GameCreatedEvent için uygular.
    Dolayısıyla sistemde GameCreateEvent oluştuğunda bunu ele alacak Handler nesnesi MediatR çalışma zamanına bildirilmiş olur.
*/
public class GameCreatedEventHandler(ILogger<GameCreatedEventHandler> logger)
    : INotificationHandler<GameCreatedEvent>
{
    public Task Handle(GameCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogWarning("Published a domain event... {Event}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}