namespace RentAGame.Catalog.Games.Features.CreateGame;

/*
    MediatR tarafında ele alınan Command bileşeni için yazılmış Handler tarafıdır.

    İlk versiyonda CreateGameCommandHandler doğrudan IRequestHandler<CreateGameCommand,CreateGameResult>
    arayüzünden türer. CreateGameCommand record türü de yine Mediator'dan IRequest<T> arayüzünden türer.

    Query tarafında da benzer arayüzler kullanılmaktadır. 
    
    Burada ilgili bileşenlerin Commmand veya Query işleri ile ilgili olduğunu daha net göstermek için 
    Shared klasöründeki abstraction'lar (ICommandHandler ve IQueryHandler) ele alınır. Bu arayüzler,
    MediatR'daki gerekli arayüzleri implemente ederken isim olarak asıl ilgilenilen süreci işaret etmemizi sağlarlar.
*/

public record CreateGameCommand(GameDto Game) : ICommand<CreateGameResult>;

public record CreateGameResult(Guid Id);

public class CreateGameHandler(CatalogDbContext catalogDbContext)
    : ICommandHandler<CreateGameCommand, CreateGameResult>
{
    public async Task<CreateGameResult> Handle(CreateGameCommand command, CancellationToken cancellationToken)
    {
        // Command nesnesinden gelen GameDto örneğini kullanarak bir Game nesnesi örneklenir
        var newGame = Game.Create(
            command.Game.Id,
            command.Game.Title,
            command.Game.Description,
            command.Game.Programmers,
            command.Game.ListPrice,
            command.Game.ThumbnailImage,
            command.Game.Genres
        );
        // DbContext setine yeni oyun nesnesi eklenir
        catalogDbContext.Games.Add(newGame);
        // Veritabanı değişiklikleri kaydedilir
        await catalogDbContext.SaveChangesAsync(cancellationToken);
        // Id bilgisine sahip response nesnesi döndürülür
        return new CreateGameResult(newGame.Id);
    }
}

#region İlk Version

// public record CreateGameCommand(
//     string Title,
//     string Description,
//     List<string> Programmers,
//     decimal ListPrice,
//     string ThumbnailImage,
//     List<Genre> Genres
// ) : IRequest<CreateGameResult>;

// public record CreateGameResult(Guid Id);

// public class CreateGameCommandHandler
//     : IRequestHandler<CreateGameCommand, CreateGameResult>
// {
//     public Task<CreateGameResult> Handle(CreateGameCommand command, CancellationToken cancellationToken)
//     {
//         throw new NotImplementedException();
//     }
// }

#endregion