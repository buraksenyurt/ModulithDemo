namespace RentAGame.Catalog.Games.Features.DeleteGame;

public record DeleteGameCommand(Guid GameId) : ICommand<DeleteGameResult>;

public record DeleteGameResult(bool IsSuccess);

public class DeleteGameCommandValidator
    : AbstractValidator<DeleteGameCommand>
{
    public DeleteGameCommandValidator()
    {
        RuleFor(c => c.GameId).NotEmpty().WithMessage("Id is required");
    }
}

public class DeleteGameHandler(CatalogDbContext catalogDbContext)
    : ICommandHandler<DeleteGameCommand, DeleteGameResult>
{
    public async Task<DeleteGameResult> Handle(DeleteGameCommand command, CancellationToken cancellationToken)
    {
        var game = await catalogDbContext.Games.FindAsync([command.GameId], cancellationToken) ?? throw new GameNotFoundException(command.GameId);

        catalogDbContext.Games.Remove(game);
        await catalogDbContext.SaveChangesAsync(cancellationToken);
        return new DeleteGameResult(true);
    }
}