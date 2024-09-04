namespace RentAGame.Catalog.Games.Features.DeleteGame;

public record DeleteGameCommand(Guid GameId) : ICommand<DeleteGameResult>;

public record DeleteGameResult(bool IsSuccess);

public class DeleteGameHandler(CatalogDbContext catalogDbContext)
    : ICommandHandler<DeleteGameCommand, DeleteGameResult>
{
    public async Task<DeleteGameResult> Handle(DeleteGameCommand command, CancellationToken cancellationToken)
    {
        var game = await catalogDbContext.Games.FindAsync([command.GameId], cancellationToken) ?? throw new Exception($"Game object not found. Id {command.GameId}");

        catalogDbContext.Games.Remove(game);
        await catalogDbContext.SaveChangesAsync(cancellationToken);
        return new DeleteGameResult(true);
    }
}