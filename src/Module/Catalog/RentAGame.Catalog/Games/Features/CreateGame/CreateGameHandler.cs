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

// Validasyon işlemleri için AbstractValidator türevli bir bileşen kullanılıyor.
public class CreateGameCommandValidator
    : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
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

public class CreateGameHandler(CatalogDbContext catalogDbContext, IValidator<CreateGameCommand> validator, ILogger<CreateGameHandler> logger)
    : ICommandHandler<CreateGameCommand, CreateGameResult>
{
    public async Task<CreateGameResult> Handle(CreateGameCommand command, CancellationToken cancellationToken)
    {
        // Command nesnesi için tanımlanmış alan doğrulama işlemleri yapılır 
        var result = await validator.ValidateAsync(command, cancellationToken);
        // Hatalar toplanır
        var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
        if (errors.Count != 0)
        {
            // Birden fazla hata varsa ilki Exception olarak fırlatılır
            var firstError = errors.FirstOrDefault();
            logger.LogError("Validation rules violation occured. Error is {ErrorMessage}", firstError);
            throw new ValidationException(firstError);
        }

        logger.LogInformation("CreateGameCommandHandler.Handle invoked. {Command}", command);

        // Command nesnesinden gelen GameDto örneğini kullanarak bir Game nesnesi örneklenir
        var newGame = Game.Create(
            Guid.NewGuid(),
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