namespace RentAGame.Catalog.Games.Features.UpdateGame;

public record UpdateGameCommand(GameDto Game) : ICommand<UpdateGameResult>;

public record UpdateGameResult(bool IsSuccess);

public class UpdateGameHandler(CatalogDbContext catalogDbContext)
    : ICommandHandler<UpdateGameCommand, UpdateGameResult>
{
    public async Task<UpdateGameResult> Handle(UpdateGameCommand command, CancellationToken cancellationToken)
    {
        var game = await catalogDbContext.Games.FindAsync([command.Game.Id], cancellationToken) ?? throw new Exception($"Game object not found. Id {command.Game.Id}");

        game.Update(
            command.Game.Title,
            command.Game.Description,
            command.Game.Programmers,
            command.Game.ListPrice,
            command.Game.ThumbnailImage,
            command.Game.Genres
        );

        catalogDbContext.Games.Update(game);
        await catalogDbContext.SaveChangesAsync(cancellationToken);
        return new UpdateGameResult(true);
    }
}