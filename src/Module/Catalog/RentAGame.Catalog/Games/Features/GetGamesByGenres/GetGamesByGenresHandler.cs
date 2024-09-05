namespace RentAGame.Catalog.Games.Features.GetGamesByGenres;

/*
    Sistemde kayıtlı olan oyunlardan belli türde olanları döndüren handler modülü.  
*/
public record GetGamesByGenresQuery(IEnumerable<Genre> Genres) : IQuery<GetGamesByGenresResult>;

public record GetGamesByGenresResult(IEnumerable<GameDto> Games);

public class GetGamesByGenresHandler(CatalogDbContext catalogDbContext)
    : IQueryHandler<GetGamesByGenresQuery, GetGamesByGenresResult>
{
    public async Task<GetGamesByGenresResult> Handle(GetGamesByGenresQuery query, CancellationToken cancellationToken)
    {
        var games = await catalogDbContext
                    .Games
                    .AsNoTracking()
                    .Where(g => g.Genres.Any(genre => query.Genres.Contains(genre)))
                    .OrderBy(g => g.Title)
                    .ToListAsync(cancellationToken);

        var gamesDtoList = games.Adapt<List<GameDto>>();
        return new GetGamesByGenresResult(gamesDtoList);
    }
}