using MediatR;

namespace RentAGame.Catalog.Games.Features.CreateGame;

/*
    MediatR tarafında ele alınan Command bileşeni için yazılmış Handler tarafıdır.
    İlk versiyonda aşağıdaki görüldüğü gibi CreateGameCommandHandler doğrudan IRequestHandler<CreateGameCommand,CreateGameResult>
    arayüzünden türer. Query türü içinde benzer durum söz konusudur. 
    
    Burada ilgili bileşenlerin Commmand veya Query işi ile ilgili olduğunu daha net göstermek için 
    Shared klasöründeki abstraction'lar (ICommandHandler ve IQueryHandler) kullanılır.
*/
public record CreateGameCommand(
    string Title,
    string Description,
    List<string> Programmers,
    decimal ListPrice,
    string ThumbnailImage,
    List<Genre> Genres
) : IRequest<CreateGameResult>;

public record CreateGameResult(Guid Id);

// İlk versiyon
public class CreateGameCommandHandler
    : IRequestHandler<CreateGameCommand, CreateGameResult>
{
    public Task<CreateGameResult> Handle(CreateGameCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}