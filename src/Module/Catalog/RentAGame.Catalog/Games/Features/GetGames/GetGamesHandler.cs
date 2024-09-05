namespace RentAGame.Catalog.Games.Features.GetGames;

/*
    Sistemde kayıtlı olan oyun listesini döndüren handler modülü.    
*/
public record GetGamesQuery() : IQuery<GetGamesResult>;

public record GetGamesResult(IEnumerable<GameDto> Games);

public class GetGamesHandler(CatalogDbContext catalogDbContext)
    : IQueryHandler<GetGamesQuery, GetGamesResult>
{
    public async Task<GetGamesResult> Handle(GetGamesQuery query, CancellationToken cancellationToken)
    {
        var games = await catalogDbContext.Games.AsNoTracking().OrderBy(g => g.Title).ToListAsync(cancellationToken);
        // Game nesnelerinden GameDto nesnelerini üretmek için
        // Mapster isimli mapper kullanılıyor
        var gamesDtoList = games.Adapt<List<GameDto>>();
        return new GetGamesResult(gamesDtoList);
    }
}