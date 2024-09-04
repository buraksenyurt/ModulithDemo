using MediatR;

namespace RentAGame.Shared.Mediator;

public interface ICommand : ICommand<Unit>
{

}
public interface ICommand<out TResponse> : IRequest<TResponse>
{

}