namespace RentAGame.Catalog.Games.EventHandlers;

/*
    Bu event handler nesnesi de oyun liste fiyatında değişiklik olduğu zaman işletilir.
*/
public class GameListPriceChangedEventHandler(ILogger<GameListPriceChangedEventHandler> logger)
    : INotificationHandler<GameListPriceChangedEvent>
{
    public Task Handle(GameListPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogWarning("Published a domain event... {Event}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}