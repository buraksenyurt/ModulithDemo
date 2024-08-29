using MediatR;

namespace RentAGame.Shared.Domain;

public interface IEvent
    : INotification
{
    public Guid TraceId { get; set; }
    public DateTime Time { get; set; }
    public string? Type => GetType().AssemblyQualifiedName;
}