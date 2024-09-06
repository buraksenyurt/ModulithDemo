namespace RentAGame.Shared.Domain;

public interface IAggregate<T>
    : IAggregate, IEntity<T>
{

}

public interface IAggregate
    : IEntity
{
    IReadOnlyList<IEvent> Events { get; }
    IEvent[] ClearEvents();
}