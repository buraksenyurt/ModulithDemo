namespace RentAGame.Catalog.Games.Features.GetGameById;

/*
    Id bilgisine göre oyun nesnesi çeken handler   
*/
public record GetGameByIdQuery(Guid Id) : IQuery<GetGameByIdResult>;

public record GetGameByIdResult(GameDto Game);

public class GetGameByIdHandler(CatalogDbContext catalogDbContext)
    : IQueryHandler<GetGameByIdQuery, GetGameByIdResult>
{
    public async Task<GetGameByIdResult> Handle(GetGameByIdQuery query, CancellationToken cancellationToken)
    {
        var game = await catalogDbContext
            .Games
            .AsNoTracking()
            // Aranan Entity verisi üzerinde değişiklik yapılmayacağı için FindAsync yerine
            // SingleOrDefaultAsync kullanımı tercih edilir.
            .SingleOrDefaultAsync(g => g.Id == query.Id, cancellationToken) ?? throw new GameNotFoundException(query.Id);

        var gameDto = game.Adapt<GameDto>();
        return new GetGameByIdResult(gameDto);
    }
}