namespace RentAGame.Catalog.Games.Features.GetGamesByPriceRange;

/*
    Sistemde kayıtlı olan oyunları, belli bir liste fiyatı aralığına göre döndüren handler modülüdür
*/
public record GetGamesByPriceRangeQuery(decimal MinValue, decimal MaxValue) : IQuery<GetGamesByPriceRangeResult>;

public record GetGamesByPriceRangeResult(IEnumerable<GameDto> Games);

public class GetGamesByPriceRangeHandler(CatalogDbContext catalogDbContext)
    : IQueryHandler<GetGamesByPriceRangeQuery, GetGamesByPriceRangeResult>
{
    public async Task<GetGamesByPriceRangeResult> Handle(GetGamesByPriceRangeQuery query, CancellationToken cancellationToken)
    {
        var games = await catalogDbContext
                    .Games
                    .AsNoTracking()
                    .Where(g => g.ListPrice >= query.MinValue && g.ListPrice <= query.MaxValue)
                    .OrderBy(g => g.ListPrice)
                    .ToListAsync(cancellationToken);

        var gamesDtoList = games.Adapt<List<GameDto>>();
        return new GetGamesByPriceRangeResult(gamesDtoList);
    }
}