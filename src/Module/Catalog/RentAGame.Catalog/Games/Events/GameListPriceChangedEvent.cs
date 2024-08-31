namespace RentAGame.Catalog.Games.Events;

public record GameListPriceChangedEvent(Game Game) : IEvent;