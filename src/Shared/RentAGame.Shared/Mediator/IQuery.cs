using MediatR;

namespace RentAGame.Shared.Mediator;

public interface IQuery<out T> : IRequest<T> where T : notnull
{

}