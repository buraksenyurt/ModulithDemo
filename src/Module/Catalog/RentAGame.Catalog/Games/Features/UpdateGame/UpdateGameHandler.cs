using RentAGame.Catalog.Games.Features.CreateGame;

namespace RentAGame.Catalog.Games.Features.UpdateGame;

public record UpdateGameCommand(GameDto Game) : ICommand<UpdateGameResult>;

public record UpdateGameResult(bool IsSuccess);

public class UpdateGameCommandValidator
    : AbstractValidator<UpdateGameCommand>
{
    public UpdateGameCommandValidator()
    {
        RuleFor(c => c.Game.Id).NotEmpty().WithMessage("Id is required");

        RuleFor(c => c.Game.Title)
            .NotEmpty()
            .WithMessage("Game title is required")
            .MaximumLength(50)
            .WithMessage("Max title length is 50 char");

        RuleFor(c => c.Game.Description)
            .NotEmpty()
            .WithMessage("Game description is required")
            .MaximumLength(300)
            .MinimumLength(20)
            .WithMessage("Description length must be between 50 and 300 chars");

        RuleFor(c => c.Game.ListPrice)
            .GreaterThan(0)
            .WithMessage("List price must be greater than 0 coin");

        RuleFor(c => c.Game.Programmers)
            .NotEmpty()
            .WithMessage("Must include at least one programmer");

        RuleFor(c => c.Game.Genres)
            .NotEmpty()
            .WithMessage("Must include at least one genre");

        RuleFor(c => c.Game.ThumbnailImage)
            .NotEmpty()
            .WithMessage("Thumbnail image is required");
    }
}

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