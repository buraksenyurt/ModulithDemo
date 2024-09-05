namespace RentAGame.Catalog.Games.Features.GetGamesByGenres;

public record GetGamesByGenresRequest(IEnumerable<Genre> Genres);
public record GetGamesByGenresResponse(IEnumerable<GameDto> Games);

public class GetGamesByGenresEndpoint
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/games/genre", async (GetGamesByGenresRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetGamesByGenresQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetGamesByGenresResponse>();
            return Results.Ok(response);
        })
        .WithName("GetGamesByGenres")
        .Produces<GetGamesByGenresResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Games by Genres")
        .WithDescription("Get all games by selected genres");
    }
}