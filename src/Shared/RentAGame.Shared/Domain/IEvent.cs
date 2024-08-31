using MediatR;

namespace RentAGame.Shared.Domain;

public interface IEvent
    : INotification
{
    Guid TraceId => Guid.NewGuid();
    public DateTime Time => DateTime.UtcNow;
    public string? Type => GetType().AssemblyQualifiedName;
}