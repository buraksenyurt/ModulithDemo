/*
    Api tarafındaki talepleri yönetmek için Carter Nuget paketi kullanılıyor.
    Aşağıdaki ICarterModule arayüzünü implemente eden CreateGameEndpoint,
    HTTP Post ile /games adresine gelen isteğe karşı yeni bir oyun bilgisi oluşturmak için gerekli işlemleri yapıyor.
*/

namespace RentAGame.Catalog.Games.Features.CreateGame;

// Request talebini temsil eden Record nesnesi.
public record CreateGameRequest(GameDto Game);

// Response nesnesi
public record CreateGameResponse(Guid Id);

public class CreateGameEndpoint
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Post metodunun ele alındığı bölüm
        app.MapPost("/games", async (CreateGameRequest request, ISender sender) =>
        {
            // MediatR tarafında ilgili command nesnesini göndermek için gerekli map işlemi
            var command = request.Adapt<CreateGameCommand>();
            // Oluşturulan command nesnesi MediatR'a gönderilir
            var result = await sender.Send(command);
            // Elde edilen cevap response nesnesine dönüştürülür
            var response = result.Adapt<CreateGameResponse>();
            // HTTP 201 Created mesajı ile birlikte geri döndürülür.
            return Results.Created($"/games/{response.Id}", response);
        })
        .WithName("CreateGame")
        .Produces<CreateGameResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new game")
        .WithDescription("Create a new game on Rent A Game system");
    }
}