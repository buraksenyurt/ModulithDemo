namespace RentAGame.Shared.Domain;

public interface IAggregate<T>
    : IEntity<T>, IEntity
{

}

public interface IAggregate
    : IEntity
{
    IReadOnlyList<IEvent> Events { get; }
    IEvent[] ClearEvents();
}