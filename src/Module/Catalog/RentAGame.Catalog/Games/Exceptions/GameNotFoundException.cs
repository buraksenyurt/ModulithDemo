using RentAGame.Shared.Exceptions;

namespace RentAGame.Catalog.Games.Exceptions;

public class GameNotFoundException(Guid gameId) : NotFoundException("Game", gameId)
{
}
