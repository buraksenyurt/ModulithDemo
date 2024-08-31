namespace RentAGame.Catalog.Games.Events;

public record GameCreatedEvent(Game Game) : IEvent;