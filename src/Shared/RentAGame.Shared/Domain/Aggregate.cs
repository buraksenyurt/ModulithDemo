namespace RentAGame.Shared.Domain;

public abstract class Aggeregate<T>
    : Entity<T>, IAggregate<T>
{
    private readonly IList<IEvent> _events = [];

    public IReadOnlyList<IEvent> Events => _events.AsReadOnly();

    public void AddEvent(IEvent e)
    {
        _events.Add(e);
    }

    public IEvent[] ClearEvents()
    {
        var events = _events.ToArray();
        _events.Clear();
        return events;
    }
}